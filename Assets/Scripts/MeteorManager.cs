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
        GameObject newMeteor = (GameObject)Instantiate(meteor, new Vector3(Random.Range(-11, 11), Random.Range(-0, 4), 0), transform.rotation);
        newMeteor.SendMessage("SetUp", this.gameObject);
        meteorList.Add(newMeteor);
    }

    void OnBeat(int beatNum)
    {
        // Moves each meteor on beat
        float temp = Random.Range(0, 5);
        if(temp < 1)
        {
            Invoke("SpawnMeteor", 0);
        }
        foreach (GameObject m in meteorList)
        {
            m.SendMessage("incrementBeat");
        }

    }

    // Removes specified meteor
    void Remove(GameObject toRemove)
    {
        meteorList.Remove(toRemove);
    }
}
