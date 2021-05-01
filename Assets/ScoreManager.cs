using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    static string curScene = "Recoil Only";
    static int numSwitches = 1;
    string[] scenes = new string[] { "TeleportOnly","Recoil Only", "Hackeysack", "FourQuadsCopy"   };
    public bool justWin;
    bool isGauntlet;
    float neededScore = 100;
    public static float score = 0;
    public TextMeshPro scoreIndicator;
    public string nextSceneName;
    Image bar;
    float startingScore;
    // Start is called before the first frame update
    void Start()
    {
        startingScore = score - (score % neededScore);
        bar = GetComponent<Image>();
        isGauntlet = ButtonScript.isGauntlet;  
        if (!isGauntlet)
        {
            bar.fillAmount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (justWin)
        {
            score += 30;
            justWin = false;
        }
        if (ButtonScript.isGauntlet)
        {
            bar.fillAmount = (ScoreManager.score % neededScore) / neededScore;
            if (ScoreManager.score - startingScore >= neededScore)
            {
                string oldScene = curScene + "";
                curScene = scenes[numSwitches%scenes.Length];
                numSwitches++;
                if (numSwitches % scenes.Length == 0)
                {
                    reshuffle(scenes);
                }
                string sceneToGo = curScene;
                if (!SceneManager.GetActiveScene().name.Contains("tutorial") && ButtonScript.tutorial)
                {
                    sceneToGo = ButtonScript.tutorialNames[sceneToGo];
                }
                SceneManager.LoadScene(sceneToGo);
                LiveOnLoad.name = sceneToGo;
                //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToGo));
                //SceneManager.UnloadSceneAsync(oldScene);
            }
        }

        scoreIndicator.text = "Score: " + score.ToString("F2");
    }
    void reshuffle(string[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

}

