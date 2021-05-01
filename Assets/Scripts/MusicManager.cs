using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public float perfectWindowLength= .06F, goodWindowLength= .1F, okWindowLength= .15F, beatsPerMeasure = 4.0F;
    public GameObject[] beatReceivers;
    public GameObject[] actionBeatReceivers;
    [Header("Control When Beats Happen (Seconds)")]
    float initialDelay = 0.461539F/2F;  // How long before the music starts 
    public float betweenBeatDelay = .461538462F; // How long in between beats
    public ActionRatingController arIndicator;
    [Header("The interval difference between beats. For example, if we want 4 16th notes, put 16,16,16,16")]
    public float[] beatWaitTimes;
    public GameObject beatPredictor, beatPredictor2;

    public GameObject cameraObject;
    //bool black = false;
    public AudioSource source;
    public static List<BeatObject> beatPredictTimes = null;
    public static List<BeatObject> oldBeatPredictTimes = null;
    public ScoreManager scoreManager;
    public float perfectScoreInc = .2F;
    static int beats = 0;
    int beatStart = -1;
    static bool grandfathering = false;
    static int curPredIndex = 0, curGrampIndex = 0;
    //bool start = false;
    private void Start()
    {

        source = GameObject.Find("SoundPlayer").GetComponent<AudioSource>();
        beats = (int)(source.time / betweenBeatDelay + .5);
        beatStart = beats;
       if (beatPredictTimes != null)
        {
            grandfathering = true;
        }
        curGrampIndex = curPredIndex;
        float val = initialDelay;
        curPredIndex = 0;
        int i = 0;
        beatPredictTimes = new List<BeatObject>();

        while (val < source.clip.length)
        {

            if (val < source.time)
            {
                curPredIndex++;
            }
            beatPredictTimes.Add(new BeatObject(val, i));
            val += (4.0F / beatWaitTimes[i]) * betweenBeatDelay;//1 quarter note is 1 beat
            i = (i + 1) % beatWaitTimes.Length;
        }

        if (!grandfathering)
        {
            oldBeatPredictTimes = beatPredictTimes;

        }


    }
   // bool beatAnimed = false;
    public float beatLTime = 0;

    //static float startTime = 0;
    static float lastSTime = -1;
    float getSourceTime()
    {
        float sTime = source.time;

        print("AWERF" + sTime + "  " + lastSTime);
        if (sTime < lastSTime - 1) //restarted song
        {
            beats = 0;
            curPredIndex = 0;
            curGrampIndex = 0;
            foreach (BeatObject b in beatPredictTimes)
            {
                b.acted = false;
                b.animed = false;
            }
            foreach (BeatObject b in oldBeatPredictTimes)
            {
                b.acted = false;
                b.animed = false;
            }
        }
        lastSTime = sTime;
        return sTime;
    }
    private void Update()
    {
        int lastBeats = beats;
        beats = (int)(source.time / betweenBeatDelay + .5);
        if (beats>lastBeats)
        {
            
            foreach (GameObject receiver in beatReceivers)
            {
                try
                {
                    // print("Outer OnBeat "+beats);
                    receiver.SendMessage("OnBeat", beats);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }
        float sourceTime = getSourceTime();
        //print("STIME " + sourceTime);
      
        float curSongTime = sourceTime;
        if (grandfathering && beats % 4 == 1 && beats - beatStart > beatsPerMeasure)
        {
            grandfathering = false;            
            oldBeatPredictTimes = beatPredictTimes;
        }
        if (grandfathering)
        {
            if (curSongTime > oldBeatPredictTimes[curGrampIndex].time) // percentage of a measure for this beat times the length of a measure assuming 4/4
            {


                Instantiate(beatPredictor, cameraObject.transform);
                Instantiate(beatPredictor2, cameraObject.transform);
                print("Grandpa Beat " + beats);
                //beatAnimed = false;
                foreach (GameObject receiver in actionBeatReceivers)
                {
                    try
                    {
                        receiver.SendMessage("OnActionBeat", beats);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                curGrampIndex += 1;
                curGrampIndex %= oldBeatPredictTimes.Count;
            }
            if (!oldBeatPredictTimes[curGrampIndex].animed && curSongTime + beatLTime > oldBeatPredictTimes[curGrampIndex].time)
            {
                oldBeatPredictTimes[curGrampIndex].animed = true;
                IndicatorOnBeat();
            }
        }
        else
        {
           
            if (!beatPredictTimes[curPredIndex].animed && curSongTime + beatLTime > beatPredictTimes[curPredIndex].time)
            {
                beatPredictTimes[curPredIndex].animed = true;
                IndicatorOnBeat();
            }
        }
        if (curSongTime > beatPredictTimes[curPredIndex].time) // percentage of a measure for this beat times the length of a measure assuming 4/4
        {
            Instantiate(beatPredictor, cameraObject.transform);
            Instantiate(beatPredictor2, cameraObject.transform);
            print("Beat " + beats);
            foreach (GameObject receiver in actionBeatReceivers)
            {
                try
                {
                    receiver.SendMessage("OnActionBeat", beats);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            curPredIndex += 1;
            curPredIndex %= beatPredictTimes.Count;

        }



    }
    void IndicatorOnBeat()
    {
        if ((beats - beatStart) >= beatsPerMeasure)
        {
            GetComponent<Animator>().SetTrigger("Beat");
        }
    }
    /**
     * Gives a number indicating how far we are from a beat. If we are closer to the last beat than the next one, returns the time since the last beat. Otherwise returns -1 * the time to the next beat
     */

    int an = 0;
    public ActionRating RateAction()
    {
        if (beats < beatsPerMeasure * 2 || source.time > source.clip.length - betweenBeatDelay * 8)
        {
            arIndicator.ShowActionRating(ActionRating.PERFECT);
            return ActionRating.PERFECT;
        }
        an++;
        float curTime = getSourceTime();
        BeatObject bestI = null;
        float bestScore = 1000000000F;
        if (grandfathering)
        {
            for (int i = 0; i < oldBeatPredictTimes.Count; i++)
            {
                if (!oldBeatPredictTimes[i].acted && Mathf.Abs(curTime - oldBeatPredictTimes[i].time) < bestScore)
                {
                    bestI = oldBeatPredictTimes[i];
                    bestScore = Mathf.Abs(curTime - oldBeatPredictTimes[i].time);
                }

            }
        }
        else
        {
            for (int i = 0; i < beatPredictTimes.Count; i++)
            {
                if (!beatPredictTimes[i].acted && Mathf.Abs(curTime - beatPredictTimes[i].time) < bestScore)
                {
                    bestI = beatPredictTimes[i];
                    bestScore = Mathf.Abs(curTime - beatPredictTimes[i].time);
                }
            }
        }
        ActionRating ar;
        if (bestI == null || bestScore > okWindowLength)
        {
            arIndicator.ShowActionRating(ActionRating.BAD);
            return ActionRating.INVALID;
        }
        bestI.acted = true;
        float timeToNextBeat = bestScore;
        if (timeToNextBeat <= perfectWindowLength)
        { 
            ar = ActionRating.PERFECT;
            ScoreManager.score += perfectScoreInc;
        }
        else
        if (timeToNextBeat <= goodWindowLength)
        {
            ar = ActionRating.GOOD;
        }
        else
        if (timeToNextBeat <= okWindowLength)
        {
            ar = ActionRating.OK;
        }
        else
        {
            ar = ActionRating.BAD;
        }
        arIndicator.ShowActionRating(ar);
        return ar;
    }

} 

public class BeatObject
{
    public bool acted;
    public bool animed;
    public float time;
    public int actionIndex;
    public BeatObject(float time, int actionIndex)
    {
        acted = animed = false;
        this.time = time;
        this.actionIndex = actionIndex;
    }
}
public enum ActionRating
{
    BAD, OK, GOOD, PERFECT, INVALID
}