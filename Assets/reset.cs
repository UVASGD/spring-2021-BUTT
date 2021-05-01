using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Balloon.curPercent = 100;
        Shoot.curRecPercent = 100;
        Shoot.curTelePercent = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
