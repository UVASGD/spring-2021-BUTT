using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int health;
    void Start() {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            health -= 1;
        }
    }

    // Reduce player health by the given quantity.
    public void Damage(int damage) {
        health -= damage;
    }

    public void Heal(int heal) {
        health += heal;
    }
}
