using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_AI : MonoBehaviour
{

    public GameObject wallPrefab, player, finnishLine;
    public int maxWalls = 10;
    public float spawnRate = .25F;
    public float spawnDistance = 20f;
    private Vector3 currentPlayerX;
    private float checkpoint = 20f;
    public List<GameObject> goals = new List<GameObject>();

    List<GameObject> spawnedWalls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerX = player.transform.position;

        //if (player.transform.position.x > checkpoint)
        //{
        print("line a " + player.transform.position.x + " " + checkpoint);
        print("line b " + spawnDistance + " " + maxWalls);

        if (spawnedWalls.Count < maxWalls && player.transform.position.x > checkpoint)
        {
            checkpoint += 10f;
            spawnDistance += 10f;

            GameObject newWall;

            Vector3 startPos;

            
            Vector3 positionFromPlayer = spawnDistance * Vector3.right;
            // spawnDistance += 10;
            newWall = (GameObject)Instantiate(wallPrefab, positionFromPlayer, this.gameObject.transform.rotation);
            //  newWall = (GameObject)Instantiate(wallPrefab, startPos, this.gameObject.transform.rotation);

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
                print("removes the wall");
                i--;
            }

        }
        //}

    }
}
