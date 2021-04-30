using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finnsihLine : MonoBehaviour

     
{
    public GameObject player;
    public float forceAmount = 1;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveAway();
        }
    }

    void moveAway()
    {
        Vector2 forceToApply;
        Transform flTransform = this.gameObject.GetComponent<Transform>();
        Transform playerTransform = player.GetComponent<Transform>();
        // operator override means we can just subtract directly
        Vector3 vecToPlayer = playerTransform.position - flTransform.position;
        // don't care about Z difference
        forceToApply = new Vector2(vecToPlayer.x, 0);
        forceToApply.Normalize();
        forceToApply = forceToApply * forceAmount;

        rb.AddForce(-forceToApply, ForceMode2D.Impulse);

        forceToApply.Normalize();
        forceToApply = forceToApply * forceAmount;

        
    }

}
