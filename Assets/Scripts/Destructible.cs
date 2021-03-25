using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maxHealth = 1;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "PlayerProjectile")
        {
            Damage(1);

        }
    }
    void Damage(int amount) {
        
            health-=amount;
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
