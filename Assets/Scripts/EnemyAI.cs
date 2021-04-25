using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float forceAmount = 3;
    public GameObject player;
    public float damage = 1;
    public int beatToJump = 1;
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
    public void Setup(GameObject player)
    {
        rb = GetComponent<Rigidbody2D>();
        this.player = player;
    }
    public void OnBeat(int beatNum) { 
        Vector2 forceToApply;
        Transform enemyTransform = this.gameObject.GetComponent<Transform>();
        Transform playerTransform = player.GetComponent<Transform>();
        // operator override means we can just subtract directly
        Vector3 vecToPlayer = playerTransform.position - enemyTransform.position;
        // don't care about Z difference
        forceToApply = new Vector2(vecToPlayer.x, vecToPlayer.y);
        forceToApply.Normalize();
        forceToApply = forceToApply* forceAmount;

        if (beatNum % 2 == beatToJump)
        {
            rb.AddForce(forceToApply, ForceMode2D.Impulse);
        }
       
        
       forceToApply.Normalize();
       forceToApply = forceToApply * forceAmount;

       rb.AddForce(forceToApply);

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("Damage", damage);
        }
    }
}