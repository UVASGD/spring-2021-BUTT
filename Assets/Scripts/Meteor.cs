using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    int beatCount;
    bool pariable;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        pariable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (beatCount == 4)
        {
            pariable = true;
        }
        if (beatCount == 8)
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
        if (beatCount == 9)
        {
            Delete();
        }
    }

    bool getPariable()
    {
        return pariable;
    }
     
    void incrementBeat()
    {
        beatCount++;
    }

    void Delete()
    {
        manager.SendMessage("Remove", this.gameObject);
        Destroy(this.gameObject);
    }
}
