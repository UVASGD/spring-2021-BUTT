using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int maxHealth = 1;
    public bool bullet = false;
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

    void Bullet()
    {
        bullet = true;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "PlayerProjectile")
        {
            Damage(1);
            if (bullet)
            {
                Destroy(c.gameObject);
            }
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
