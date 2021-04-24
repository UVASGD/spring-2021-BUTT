using System.Collections;
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
    bool black = false;
    AudioSource source;


    float beats = 0;
    private void Start()
    {
        lastActionTime = lastDisplayTime = initialDelay;
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {

        float curSongTime = source.time;
        while (curSongTime - lastDisplayTime + beatsPerMeasure * betweenBeatDelay >= (1.0/beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure)
        {
            print("Instantiating Event " + curSongTime + " "+ currentDisplayAction);
            Instantiate(beatPredictor, this.transform);
            lastDisplayTime += (1.0F / beatWaitTimes[currentDisplayAction]) * betweenBeatDelay * beatsPerMeasure;
            currentDisplayAction = (currentDisplayAction + 1) % beatWaitTimes.Length;

        }
        if (curSongTime - lastActionTime > (1.0/beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure) // percentage of a measure for this beat times the length of a measure assuming 4/4
        {

            print("Firing Event " + curSongTime+ " " + currentDisplayAction);
            foreach (GameObject receiver in actionBeatReceivers)
            {
                receiver.SendMessage("OnActionBeat", currentAction);
            }
            lastActionTime += (1.0F / beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure;
            currentAction = (currentAction + 1) % beatWaitTimes.Length;
            SwitchColor();
        }
        if (curSongTime - initialDelay - beats * betweenBeatDelay >= 0)
        {
            foreach (GameObject receiver in beatReceivers)
            {
                receiver.SendMessage("OnBeat", beats);
            }
            beats++;
        }
    }
    void SwitchColor()
    {
        GetComponent<SpriteRenderer>().color = black ? new Color(1, 1, 1) : new Color(0, 0, 0);
        black = !black;
    }
    /**
     * Gives a number indicating how far we are from a beat. If we are closer to the last beat than the next one, returns the time since the last beat. Otherwise returns -1 * the time to the next beat
     */
    private float TimeSinceLastBeat()
    {
        float timeSinceLastBeat = source.time - lastActionTime;
        float timeToNextBeat = (1.0F / beatWaitTimes[currentAction]) * betweenBeatDelay * beatsPerMeasure + lastActionTime - source.time;
        return timeSinceLastBeat < timeToNextBeat ? timeSinceLastBeat : -timeToNextBeat;
        /* CODE FOR CONSTANT LENGTH BEATS:
        float timeSinceLastBeat = source.time - initialDelay - (beats - 1) * betweenBeatDelay;
        return timeSinceLastBeat < betweenBeatDelay / 2F ? timeSinceLastBeat : timeSinceLastBeat - betweenBeatDelay; //return the distance to the last beat or the next beat, whichever is closest
        */

    }

    public ActionRating RateAction()
    {
        ActionRating ar;
        float timeToNextBeat = Mathf.Abs(TimeSinceLastBeat());
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