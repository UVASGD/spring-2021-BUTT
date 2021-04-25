using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab, player;
    public int maxEnemies = 10;
    public float spawnRate = .25F;
    public float spawnDistance = 10;
    List<EnemyAI> spawnedEnemies = new List<EnemyAI>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnBeat(int beat)
    {
        if (spawnedEnemies.Count < maxEnemies && Random.value < spawnRate) //spawn randomly, but average out to spawnRate enemies per beat
        {
            float rotation = Random.value * Mathf.PI * 2;
            Vector3 positionFromPlayer = spawnDistance * new Vector3(Mathf.Sin(rotation), Mathf.Cos(rotation), 0);
            GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, player.transform.position + positionFromPlayer, this.gameObject.transform.rotation);
            EnemyAI newAI = newEnemy.GetComponent<EnemyAI>();
            newAI.Setup(player);
            spawnedEnemies.Add(newAI);
        }
        for (int i = 0; i < spawnedEnemies.Count; i++) {
            if (spawnedEnemies[i] != null) {
                spawnedEnemies[i].OnBeat(beat);
            }
            else
            {
                spawnedEnemies.RemoveAt(i);
                i--;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
