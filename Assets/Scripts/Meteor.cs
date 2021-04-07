using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    int beatCount;
    int startBeat;
    float initialPosition;
    bool pariable;
    GameObject playerInside;
    public int damage = 10;
    public GameObject manager;
    public float deathLength = .25F;
    float deathTimer = -1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        startBeat = 2;
        initialPosition = transform.position.y;
        playerInside = null;
        pariable = false;
        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, initialPosition - beatCount, transform.position.z);
        // Changes state of meteor on beat
        if (deathTimer != -1 && Time.time - deathTimer > deathLength)
        {
            Delete();
        } 
        if (beatCount == startBeat)
        {
            pariable = true;
        }
        if (beatCount == startBeat + 3)
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
       
    }

    bool getPariable()
    {
        return pariable;
    }

    void SetUp(GameObject MeteorManager)
    {
        manager = MeteorManager;
    }

    // Checks if Player is inside of Meteor and saves it's object in memory
    void OnTriggerStay2D(Collider2D o)
    {
        if(playerInside == null && o.gameObject.name == "Player")
        {
            playerInside = o.gameObject;
        }
    }

    // If Player leaves Meteor, meteor removes player object from memory
    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.name == "Player")
        {
            playerInside = null;
        }
    }
    
    // Message sent from MeteorManager to increment beat
    void incrementBeat()
    {
        beatCount++;
        if (beatCount == startBeat + 4 && deathTimer == -1)
        {
            // Checks if player is still colliding with Meteor. If so, deal damage to player.
            if (playerInside != null)
            {
                playerInside.SendMessage("Damage", damage);
            }
            deathTimer = Time.time;
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }

    // Tells MeteorManager to remove current object from list
    // Destroys current object
    void Delete()
    {
        manager.SendMessage("Remove", this.gameObject);
        Destroy(this.gameObject);
    }
}
