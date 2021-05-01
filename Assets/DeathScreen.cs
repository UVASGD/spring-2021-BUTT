using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshPro>().text += ScoreManager.score;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > 4)
        {
            string oldScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("opening_screen");
            SceneManager.UnloadSceneAsync(oldScene);
        }
    }
}
