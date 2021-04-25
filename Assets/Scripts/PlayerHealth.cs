using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public float health;
    public bool healthRegen = false;
    void Start() {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            health -= 1;
        }
        if (healthRegen && health < maxHealth)
            health += 0.01F;
    }

    // Reduce player health by the given quantity.
    public void LowerHealth(int damage) {
        health -= damage;
        if (health < 0)
            health = 0;
    }

    public void RaiseHealth(int heal) {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
    }
}
