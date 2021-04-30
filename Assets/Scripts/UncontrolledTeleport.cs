using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class UncontrolledTeleport : MonoBehaviour
{
    public float teleLength;
    Rigidbody2D rb;
    Shoot shootScript;
    Movement movementScript;
    public GameObject teleIndicator;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
        shootScript = GetComponent<Shoot>();
        
    }
    public void OnBeat(int beat)
    {
       
        if (beat % 2 == 1)
        {
            /*
            Vector3 tempTeleport = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vec2Mouse = tempTeleport - transform.position;
            vec2Mouse.z = 0;
            vec2Mouse.Normalize();
            transform.position += vec2Mouse * teleLength;
            rb.velocity = new Vector3(0, 0, 0);*/
            movementScript.Teleport(.999F);
            /*Vector3 tempTeleport = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          Vector3 vec2Mouse = tempTeleport - transform.position;
          // don't care about Z difference
          Vector2 forceToApply = new Vector2(vec2Mouse.x, vec2Mouse.y);
          forceToApply.Normalize();
          forceToApply = forceToApply * forceAmount;
          rb.AddForce(forceToApply, ForceMode2D.Impulse);
          */
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*Vector3 tempTeleport = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec2Mouse = tempTeleport - transform.position;
        vec2Mouse.z = 0;
        vec2Mouse.Normalize();
        teleIndicator.transform.position = transform.position + teleLength * vec2Mouse;
        teleIndicator.transform.position -= new Vector3(0, 0, teleIndicator.transform.position.z);
        */

    }
}
