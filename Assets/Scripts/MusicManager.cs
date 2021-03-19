using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource music;
    float length = 5.538F;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
    }
    float timer= -1;
    bool timing= false;
    void Pause()
    {
        timing = true;
        music.volume = .3F;
        timer = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (timing && Time.time - timer > length)
        {
            timing = false;
            music.volume = 1;
        }
    }
}
