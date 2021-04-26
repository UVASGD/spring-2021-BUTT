using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public List<GameObject> meteorList;
    public bool setPos = false;
    List<GameObject> removeList = new List<GameObject>();

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
        GameObject newMeteor = null;
        if(!setPos)
            newMeteor = (GameObject)Instantiate(meteor, new Vector3(Random.Range(-11, 11), Random.Range(-0, 4), 0), transform.rotation);
        else
        {
            int x = Random.Range(0, 4);
            if(x == 0)
                newMeteor = (GameObject)Instantiate(meteor, new Vector3(-3, 2.5F, -9), transform.rotation);
            else if(x == 1)
                newMeteor = (GameObject)Instantiate(meteor, new Vector3(3, 2.5F, -9), transform.rotation);
            else if(x == 2)
                newMeteor = (GameObject)Instantiate(meteor, new Vector3(-3, -1.5F, -9), transform.rotation);
            else
                newMeteor = (GameObject)Instantiate(meteor, new Vector3(3, -1.5F, -9), transform.rotation);
        }
        newMeteor.SendMessage("SetUp", this.gameObject);
        meteorList.Add(newMeteor);
    }

    void OnBeat(int beatNum)
    {
        // Moves each meteor on beat
        float temp = Random.Range(0, 5);
        if(beatNum %4 == 0 && temp < 4)
        {
            Invoke("SpawnMeteor", 0);
        }
        foreach (GameObject m in meteorList)
        {
            m.SendMessage("incrementBeat");
        }
        for (int i = 0; i < removeList.Count;)
        {
            GameObject rem = removeList[i];
            meteorList.Remove(rem);
            removeList.Remove(rem);
            Destroy(rem);
        }

    }
    // Removes specified meteor
    void Remove(GameObject toRemove)
    {
        removeList.Add(toRemove);
    }
}
