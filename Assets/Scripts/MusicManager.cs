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
    public GameObject beatPredictor;
    float lastActionTime;
    int currentAction = 0;
    float lastDisplayTime;
    int currentDisplayAction = 0;
    public GameObject cameraObject;
    //bool black = false;
    AudioSource source;
    public List<float> beatTimes;
    
    float beats = 0;
    private void Start()
    {
        lastActionTime = lastDisplayTime = initialDelay;
        source = GetComponent<AudioSource>();
        beatTimes = new List<float>();

    }
    private void Update()
    {
        if (beatTimes.Count > 0 && Mathf.Abs(source.time - beatTimes[0]) > beatsPerMeasure * betweenBeatDelay)
        {
            beatTimes.RemoveAt(0);
        }
        float curSongTime = source.time;
        while (curSongTime - lastDisplayTime + beatsPerMeasure * betweenBeatDelay >= (1.0/beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure)
        {
            beatTimes.Add((1.0F / beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure + lastDisplayTime);
            Instantiate(beatPredictor, cameraObject.transform);
            lastDisplayTime += (1.0F / beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure;
            currentDisplayAction = (currentDisplayAction + 1) % beatWaitTimes.Length;

        }
        if (curSongTime - lastActionTime > (1.0/beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure) // percentage of a measure for this beat times the length of a measure assuming 4/4
        {

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
            IndicatorOnBeat();
        }
        if (curSongTime - initialDelay - beats * betweenBeatDelay >= 0)
        {
            foreach (GameObject receiver in beatReceivers)
            {
                try
                {
                    receiver.SendMessage("OnBeat", beats);
                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            beats++;
        }
    }
    void IndicatorOnBeat()
    {
        GetComponent<Animator>().SetTrigger("Beat");

//        GetComponent<SpriteRenderer>().color = black ? new Color(1, 1, 1) : new Color(0, 0, 0);
//        black = !black;
    }
    /**
     * Gives a number indicating how far we are from a beat. If we are closer to the last beat than the next one, returns the time since the last beat. Otherwise returns -1 * the time to the next beat
     */
    float TimeSinceLastBeat()
    {
        float timeSinceLastBeat = source.time - lastActionTime;
        float timeToNextBeat = (1.0F / beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure + lastActionTime - source.time;
        return timeSinceLastBeat < timeToNextBeat ? timeSinceLastBeat : -timeToNextBeat;
       

    }
    int an = 0;
    public ActionRating RateAction()
    {
        an++;
        print("AN " + an);
        float curTime = source.time;
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