using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float forceAmount = 3;
    public float damage = 1;
    public GameObject goal;
    public GameObject manager;
    public int beatToJump = 1;
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
    public void Setup(GameObject player, GameObject Egoal, GameObject EnemyManager)
    {
        rb = GetComponent<Rigidbody2D>();
        manager = EnemyManager;
        this.player = player;
        if (Egoal != null)
        {
            goal = Egoal;
        }
        else
        {
            goal = player;
        }
    }
    public void OnBeat(int beatNum) { 
        Vector2 forceToApply;
        Transform enemyTransform = this.gameObject.GetComponent<Transform>();
        Transform goalTransform = goal.GetComponent<Transform>();
        // operator override means we can just subtract directly
        Vector3 vecToGoal = goalTransform.position - enemyTransform.position;
        // don't care about Z difference
        forceToApply = new Vector2(vecToGoal.x, vecToGoal.y);
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
            player.SendMessage("Damage", damage);
            Destroy(this.gameObject);
        }
    }
}