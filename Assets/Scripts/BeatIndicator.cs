using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{
    bool black = false;
    
    public void OnBeat(float beats)
    {
        GetComponent<SpriteRenderer>().color = black ? new Color(1, 1, 1) : new Color(0, 0, 0);
        black = !black;
        if (beats%4 == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
