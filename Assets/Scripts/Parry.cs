using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{



    public int maxHealth = 10;
    public float parryLeniencySeconds = .025F;
    
    public float cooldownLengthSeconds = 1;

    [Header("Debug Info")]
    float cooldownTimer = 0;
    float leniencyTimer = -1;
    float parryTimer = 0;
    public int ammo;
    
    public bool parrying;
    private SpriteRenderer sprite;
    int health;
    int lastDamage;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parryTimer > 0)
        {
            parryTimer -= Time.deltaTime; //gross and bad, should be using Time.time, but makes the code more similar to what it was before
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (leniencyTimer > 0)
        {
            leniencyTimer -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(1) && cooldownTimer==0)
        {
            if (leniencyTimer > 0) //got hit before parrying
            {
                leniencyTimer = -1;
                ammo++;
            }
            else
            {
                sprite.color = new Color(0, 1, 1, 1);
                parrying = true;
                parryTimer = parryLeniencySeconds;
            }
        }
        if (parryTimer == 0 && parrying)
        {
            sprite.color = new Color(1, 1, 1, 1);
            cooldownTimer = cooldownLengthSeconds;
            parrying = false;
        }

        if (leniencyTimer == 0)
        {
            leniencyTimer = -1;
            //take damage
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

    }
    void Damage(int damage)
    {

        if (parrying)
        {
            parrying = false;
            cooldownTimer = cooldownLengthSeconds;
            sprite.color = new Color(1, 1, 1, 1);
            ammo++;
        }
        else
        {
            leniencyTimer = parryLeniencySeconds;
        }
    }
}
