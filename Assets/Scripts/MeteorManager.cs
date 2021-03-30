using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public List<GameObject> meteorList;
    public GameObject meteor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMeteor()
    {
        GameObject temp = meteor;
        Instantiate(temp, new Vector3(Random.Range(-11, 11), Random.Range(-4, 4), 0), transform.rotation);
        temp.gameObject.SendMessage("SetUp", this);
        meteorList.Add(temp);
    }

    void OnBeat(int beatNum)
    {
        // Moves each meteor on beat
        float x = 0;
        Invoke("SpawnMeteor", x);
        foreach (GameObject m in meteorList)
        {
            Debug.Log("meteor");
            m.SendMessage("incrementBeat");
        }

    }

    // Removes specified meteor
    void Remove(GameObject toRemove)
    {
        meteorList.Remove(toRemove);
    }
}
