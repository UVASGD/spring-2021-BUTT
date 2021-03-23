using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private TextMesh health_display;
    public GameObject player;
    // public PlayerHealth player_health;
    // Start is called before the first frame update
    void Start() {
        health_display = GetComponent<TextMesh>();
        health_display.text = ""+player.GetComponent<PlayerHealth>().health;
        // health_display.text = player_health.health;
        // health_display.text = "hi";
    }

    // Update is called once per frame
    void Update() {
        health_display.text = ""+player.GetComponent<PlayerHealth>().health;
    }
}
