using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private Image healthbar;
    public GameObject player;
    private int maxHealth;
    // Start is called before the first frame update
    void Start() {
        healthbar = GetComponent<Image>();
        maxHealth = player.GetComponent<PlayerHealth>().maxHealth;
    }

    // Update is called once per frame
    void Update() {
        float health = player.GetComponent<PlayerHealth>().health;
        healthbar.fillAmount = health / maxHealth;
    }
}