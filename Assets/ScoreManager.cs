using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    bool isGauntlet;
    public float neededScore = 100;
    public float score = 0;
    public TextMeshPro scoreIndicator;
    public string nextSceneName;
    Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        isGauntlet = ButtonScript.isGauntlet;  
        if (!isGauntlet)
        {
            bar.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGauntlet)
        {
            bar.fillAmount = score / neededScore;
            if (score >= neededScore)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        scoreIndicator.text = "Score: " + score;
    }
}

