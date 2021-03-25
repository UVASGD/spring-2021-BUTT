using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    int beatCount;
    int startBeat;
    float initialPosition;
    bool pariable;
    Transform transform;
    GameObject playerInside;
    public int damage = 1;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        startBeat = 2;
        transform = GetComponent<Transform>();
        initialPosition = transform.position.y;
        playerInside = null;
        pariable = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, initialPosition - beatCount, transform.position.z);
        // Changes state of meteor on beat

        if (beatCount == startBeat)
        {
            pariable = true;
        }
        if (beatCount == startBeat + 3)
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }
        if (beatCount == startBeat + 4)
        {
            if(playerInside != null)
            {
                playerInside.SendMessage("Damage", damage);
            }
            Delete();
        }
    }

    bool getPariable()
    {
        return pariable;
    }

    void OnTriggerStay(Collider o)
    {
        Debug.Log("Test");
        if(o.gameObject.name == "Player")
        {
            playerInside = o.gameObject;
        }
    }

    void OnTriggerExit(Collider o)
    {
        if(o.gameObject.name == "Player")
        {
            playerInside = null;
        }
    }
    
    // Message sent from MeteorManager to increment beat
    void incrementBeat()
    {
        beatCount++;
    }

    // Tells MeteorManager to remove current object from list
    // Destroys current object
    void Delete()
    {
        manager.SendMessage("Remove", this.gameObject);
        Destroy(this.gameObject);
    }
}
