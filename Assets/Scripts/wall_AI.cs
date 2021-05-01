using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_AI : MonoBehaviour
{

    public GameObject wallPrefab, player;
    public int maxWalls = 10;
    public float spawnRate = .25F;
    public float spawnDistance = 10;
    public List<GameObject> goals = new List<GameObject>();

    List<GameObject> spawnedWalls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float tempSpawnRate = spawnRate;
        tempSpawnRate -= Random.value;
        while (spawnedWalls.Count < maxWalls && tempSpawnRate > 0)
        {
            tempSpawnRate -= Random.value;
            GameObject newWall;

            Vector3 startPos;
            
            float rotation = Random.value * Mathf.PI * 2;
            Vector3 positionFromPlayer = spawnDistance * Vector3.right;
            spawnDistance += 10;
            newWall = (GameObject)Instantiate(wallPrefab, player.transform.position + positionFromPlayer, this.gameObject.transform.rotation);
          //  newWall = (GameObject)Instantiate(wallPrefab, startPos, this.gameObject.transform.rotation);

           // Wall_moves newAI = newWall.GetComponent<Wall_moves>();
            int minIndex = -1;
            float dist = float.MaxValue;
            for (int i = 0; i < goals.Count; i++)
            {
                float temp = Vector3.Distance(goals[i].transform.position, newWall.transform.position);
                if (temp < dist)
                {
                    dist = temp;
                    minIndex = i;
                }
            }
           // if (minIndex == -1)
            //{
            //    newAI.Setup(player, null, this.gameObject);
           // }
           // else
          //  {
         //       newAI.Setup(player, goals[minIndex], this.gameObject);
          //  }
            spawnedWalls.Add(newWall);
        }
        for (int i = 0; i < spawnedWalls.Count; i++)
        {
            if (spawnedWalls[i] != null)
            {
               // spawnedWalls[i].SendMessage("OnBeat", beat);
            }
            else
            {
               // ScoreManager.score += killScoreInc;
                spawnedWalls.RemoveAt(i);
                i--;
            }

        }
    }
}
