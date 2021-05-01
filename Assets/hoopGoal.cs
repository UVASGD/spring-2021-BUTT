using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoopGoal : MonoBehaviour
{
    private int rotates;
    private bool visible;
    private int timeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotates > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -3));
            rotates--;
        }
        if(timeToSpawn > 0)
        {
            timeToSpawn--;
        }
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (visible && o.gameObject.name == "Player")
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 0);
            ScoreManager.score += 20;
            visible = false;
            timeToSpawn = 120;
        }
    }

    void Respawn()
    {
        transform.position = new Vector3(Random.Range(-8.5F, 8.5F), Random.Range(-2, 3), transform.position.z);
        Color temp = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 1);
        visible = true;
    }

    void OnBeat()
    {
        rotates = 20;
        if (timeToSpawn == 0 && !visible)
        {
            Respawn();
        }
    }
}
