using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public int damage = 10;
    public GameObject manager;
    public int beatsToDrop = 4;
    public bool progressiveOpacity;

    int beatCount = 0;
    float initialPosition;
    GameObject playerInside;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        initialPosition = transform.position.y;
        playerInside = null;
        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, initialPosition - beatCount, transform.position.z);
        // Changes state of meteor on beat

        Color temp;
        if (beatCount == 0)
        {
            temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 0.33F);
        }
        if (beatCount == 1)
        {
            temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 0.8F);
        }
        if (beatCount == 2)
        {
            temp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 1);
            GetComponent<CircleCollider2D>().enabled = true;
        }
       
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
    bool black = true;
    // Message sent from MeteorManager to increment beat
    void incrementBeat()
    {
        beatCount++;
        GetComponent<SpriteRenderer>().color = black ? new Color(1, 1, 1) : new Color(1, 1, 0);
        black = !black;

        if (beatCount == beatsToDrop - 1 )
        {
            // Checks if player is still colliding with Meteor. If so, deal damage to player.
            if (playerInside != null)
            {
                playerInside.SendMessage("Damage", damage);
            }
            Delete();
        }
    }

    // Tells MeteorManager to remove current object from list
    // Destroys current object
    void Delete()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        manager.SendMessage("Remove", this.gameObject);
    }
}
