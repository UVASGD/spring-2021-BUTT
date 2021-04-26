using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float teleportLeniency = .06F;
    public float forceAmount = 100;
    public MusicManager musicManager;
    public bool disableMovement = false;
    public bool fourQuads = false;
    public bool teleEnabled = true;

    private Rigidbody2D rb;
    private Transform tf;
   
    private Vector3 tempTeleport;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        if (!disableMovement)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(new Vector2(0, forceAmount));
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(new Vector2(-forceAmount, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(new Vector2(0, -forceAmount));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector2(forceAmount, 0));
            }
        }
        if (teleEnabled)
        {
            tempTeleport = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (musicManager.RateAction() != ActionRating.INVALID)
                {
                    Teleport(1);
                }
            }
        }
        
    }
    public void Teleport(float percent)
    {
        tempTeleport = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (fourQuads)
        {
            if (tempTeleport.x < 0 && tempTeleport.y >= 0.4F)
            {
                tempTeleport.x = -3;
                tempTeleport.y = 2.5F;
            }
            else if (tempTeleport.x >= 0 && tempTeleport.y >= 0.4F)
            {
                tempTeleport.x = 3;
                tempTeleport.y = 2.5F;
            }
            else if (tempTeleport.x < 0 && tempTeleport.y < 0.4F)
            {
                tempTeleport.x = -3;
                tempTeleport.y = -1.5F;
            }
            else
            {
                tempTeleport.x = 3;
                tempTeleport.y = -1.5F;
            }
            tf.position = new Vector3(tempTeleport.x, tempTeleport.y, 0);
        }
        else
        {
            Vector3 vec2Mouse = tempTeleport - tf.position;
            vec2Mouse.z = 0;
            tf.position += percent * vec2Mouse;
        }
        rb.velocity = new Vector3(0, 0, 0);
    }
}