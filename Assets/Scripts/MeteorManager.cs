using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public List<GameObject> meteors;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBeat(int beatNum)
    {
        foreach(GameObject m in meteors)
        {
            m.SendMessage("incrementBeat");
        }
        
    } 

    void Remove(GameObject toRemove)
    {
        meteors.Remove(toRemove);
    }
}
