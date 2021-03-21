using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public const int Leniency = 30;
    public const int Cooldown = 120;
    public int ammo;
    public int frameCounter;
    public int cooldownCounter;
    public int leniencyCounter;
    public bool parrying;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        ammo = 0;
        frameCounter = 0;
        parrying = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownCounter==0)
        {
            if (leniencyCounter > 0)
            {
                leniencyCounter = 0;
                ammo++;
            }
            else
            {
                sprite.color = new Color(0, 1, 1, 1);
                parrying = true;
                frameCounter = Leniency + 1;
                cooldownCounter = Cooldown + 1;
            }
        }
        if (frameCounter > 0)
        {
            frameCounter--;
            if (frameCounter == 0 && parrying)
            {
                sprite.color = new Color(1, 1, 1, 1);
                parrying = false;
            }
        }
        if (cooldownCounter > 0)
            cooldownCounter--;
        if (leniencyCounter > 0)
        {
            leniencyCounter--;
            if (leniencyCounter == 0)
            {
                //get hurt
            }
        }
    }
    void ProjectileHit()
    {
        if (parrying)
        {
            parrying = false;
            frameCounter = 0;
            sprite.color = new Color(1, 1, 1, 1);
            ammo++;
        }
        else
            leniencyCounter = Leniency;
    }
}
