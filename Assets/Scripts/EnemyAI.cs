using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float forceAmount = 3;
    public GameObject player;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnBeat(int beatNum) { 
        Vector2 forceToApply;

        Transform enemyTransform = this.gameObject.GetComponent<Transform>();
        Transform playerTransform = player.GetComponent<Transform>();
        // operator override means we can just subtract directly
         Vector3 vecToPlayer = playerTransform.position - enemyTransform.position;
         // don't care about Z difference
        forceToApply = new Vector2(vecToPlayer.x, vecToPlayer.y);
         forceToApply.Normalize();
         forceToApply = forceToApply* forceAmount;

        if (beatNum % 2 == 1)
        {

            rb.AddForce(forceToApply, ForceMode2D.Impulse);
            rb.drag = 50;

        
        }
       
        
       //forceToApply.Normalize();
        //forceToApply = forceToApply * 3;

        //rb.AddForce(forceToApply);
    }
}