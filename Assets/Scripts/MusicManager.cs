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
    public float initialDelay = 0F;  // How long before the music starts 
    public float betweenBeatDelay = .461538462F; // How long in between beats
    public ActionRatingController arIndicator;
    [Header("The interval difference between beats. For example, if we want 4 16th notes, put 16,16,16,16")]
    public float[] beatWaitTimes;
    public GameObject beatPredictor, beatPredictor2;
    static float lastActionTime = 0;
    static int currentAction = 0;
    static float lastDisplayTime;
    static int currentDisplayAction = 0;
    public GameObject cameraObject;
    //bool black = false;
    public AudioSource source;
    public List<float> beatTimes;
    public ScoreManager scoreManager;
    public float perfectScoreInc = .2F;
    static int beats = 0;
    bool start = false;
    int beatStart = -1;
    private void Start()
    {
        start = true;
        beatStart = beats;
        source = GameObject.Find("SoundPlayer").GetComponent<AudioSource>();
        beatTimes = new List<float>();
        float curBeat = beats % (int)beatsPerMeasure;
        //print("curBeat " + curBeat);
        int i;
        for (i = 0; ; i = (i + 1) % beatWaitTimes.Length)
        {
            curBeat -= beatsPerMeasure / beatWaitTimes[i];
            if (curBeat < 1F / 128F)
            {
                break;
            }
           
        }
        currentAction = i;
        currentDisplayAction = i;
        //print("Therefore " + i);
        if (lastActionTime == 0)
        {
            lastActionTime = lastDisplayTime = initialDelay;
        }
       

    }
    static int curReps;
    bool beatAnimed = false;
    public float beatLTime = 0;

    //static float startTime = 0;
    static float lastSTime = -1;
    float getSourceTime()
    {
        float sTime = source.time;
        if (sTime < lastSTime - 1) //restarted song
        {
            curReps++;
        }
        lastSTime = sTime;
        return curReps*source.clip.length + sTime;
    }
    private void Update()
    {

        float sourceTime = getSourceTime();
        //print("STIME " + sourceTime);
        if (beatTimes.Count > 0 && Mathf.Abs(sourceTime - beatTimes[0]) > beatsPerMeasure * betweenBeatDelay)
        {
            beatTimes.RemoveAt(0);
        }
        float curSongTime = sourceTime;
        while (curSongTime - lastDisplayTime + beatsPerMeasure * betweenBeatDelay >= (1.0/beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure)
        {
            beatTimes.Add((1.0F / beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure + lastDisplayTime);
            Instantiate(beatPredictor, cameraObject.transform);
            Instantiate(beatPredictor2, cameraObject.transform);
            lastDisplayTime += (1.0F / beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure;
            currentDisplayAction = (currentDisplayAction + 1) % beatWaitTimes.Length;

        }
        if (curSongTime - lastActionTime > (1.0/beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure) // percentage of a measure for this beat times the length of a measure assuming 4/4
        {
            beatAnimed = false;
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
            lastActionTime += (1.0F / beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure;
            currentAction = (currentAction + 1) % beatWaitTimes.Length;
        } 
        if (! beatAnimed && curSongTime - lastActionTime + beatLTime > (1.0 / beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure)
        {
            beatAnimed = true;
            IndicatorOnBeat();
        }

        
        if (curSongTime - initialDelay - beats * betweenBeatDelay >= 0)
        {
            if ((beats - beatStart) >= beatsPerMeasure)
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
            } else
            {
               // print("Skip!");
            }
            beats++;
        }
    }
    void IndicatorOnBeat()
    {
        if ((beats - beatStart) >= beatsPerMeasure)
        {
            GetComponent<Animator>().SetTrigger("Beat");
        }

//        GetComponent<SpriteRenderer>().color = black ? new Color(1, 1, 1) : new Color(0, 0, 0);
//        black = !black;
    }
    /**
     * Gives a number indicating how far we are from a beat. If we are closer to the last beat than the next one, returns the time since the last beat. Otherwise returns -1 * the time to the next beat
     */
    float TimeSinceLastBeat()
    {
        float timeSinceLastBeat = getSourceTime() - lastActionTime;
        float timeToNextBeat = (1.0F / beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure + lastActionTime - getSourceTime();
        return timeSinceLastBeat < timeToNextBeat ? timeSinceLastBeat : -timeToNextBeat;
       

    }
    int an = 0;
    public ActionRating RateAction()
    {
        an++;
        float curTime = getSourceTime();
        int bestI = -1;
        float bestScore = 1000000000F;
        for (int i = 0; i < beatTimes.Count; i++) {
            if (Mathf.Abs(curTime - beatTimes[i]) < bestScore)
            {
                bestI = i;
                bestScore = Mathf.Abs(curTime - beatTimes[i]);
            } 
        }
        ActionRating ar;
        if (bestI == -1 || bestScore > okWindowLength)
        {
            arIndicator.ShowActionRating(ActionRating.BAD);
            return ActionRating.INVALID;
        }
        beatTimes.RemoveAt(bestI);
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
public enum ActionRating
{
    BAD, OK, GOOD, PERFECT, INVALID
}