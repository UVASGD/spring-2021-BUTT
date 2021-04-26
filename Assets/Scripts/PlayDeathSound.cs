using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeathSound : MonoBehaviour
{
    public AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        deathSound.PlayOneShot(deathSound.clip, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
