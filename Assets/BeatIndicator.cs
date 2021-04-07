using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{
    bool black = false;
    public void OnBeat()
    {
        GetComponent<SpriteRenderer>().color = black ? new Color(1, 1, 1) : new Color(0, 0, 0);
        black = !black;
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
