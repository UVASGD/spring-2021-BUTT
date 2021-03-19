using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public GameObject[] beatReceivers;
    [Header("Control When Beats Happen (Seconds)")]
    public float initialDelay = 0F;  // How long before the music starts 
    public float betweenBeatDelay = .461538462F; // How long in between beats
    
    AudioSource source;

    float beats = 0;
    private void Update()
    {
        float curSongTime = source.time;

        if (curSongTime - initialDelay - beats * betweenBeatDelay >= 0)
        {
            foreach (GameObject receiver in beatReceivers)
            {
                receiver.SendMessage("OnBeat", beats);
            }
            beats++;

        }
    }
}