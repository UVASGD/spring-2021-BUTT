using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class UncontrolledTeleport : MonoBehaviour
{
    public float forceAmount;
    Rigidbody2D rb;
    Shoot shootScript;
    Movement movementScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
        shootScript = GetComponent<Shoot>();
        
    }
    public void OnBeat(int beat)
    {
        /*Vector3 tempTeleport = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec2Mouse = tempTeleport - transform.position;
        // don't care about Z difference
        Vector2 forceToApply = new Vector2(vec2Mouse.x, vec2Mouse.y);
        forceToApply.Normalize();
        forceToApply = forceToApply * forceAmount;
        rb.AddForce(forceToApply, ForceMode2D.Impulse);
        */
        if (beat % 2 == 0)
        {
            movementScript.Teleport(.5F);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
