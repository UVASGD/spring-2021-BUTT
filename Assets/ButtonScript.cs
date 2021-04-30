using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int beatsToSwitch;
    public GameObject container1, container2;
    public string firstLevelScene;
    public TextMeshPro timeInd;
    bool pIn = false;
    int pInBeat = -1;
    public bool loadFirstLevel, beatCounter;
    public GameObject enemy;
    GameObject[] enemies = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnBeat(int beat)
    {
        if (pIn)
            if (beatCounter)
            {
                pInBeat--;
            } else
            {
                foreach (GameObject g in enemies)
                {
                    if (g!=null)
                        g.SendMessage("OnBeat", beat);
                }
            }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (pIn)
        {
            if (beatCounter)
            {

                timeInd.text = "Countdown:" + pInBeat;
                if (pInBeat == 0)
                {
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    if (loadFirstLevel)
                    {
                        FirstLevel();
                    }
                    else
                    {
                        container1.SetActive(!container1.activeSelf);
                        container2.SetActive(!container2.activeSelf);
                    }
                }
            } else
            {
                int enemyCount = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (enemies[i] != null)
                    {
                        enemyCount++;
                    }
                }
                timeInd.text = "Enemies Left: " + enemyCount;
                if (enemyCount == 0)
                {
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    if (loadFirstLevel)
                    {
                        FirstLevel();
                    } else
                    {
                        container1.SetActive(!container1.activeSelf);
                        container2.SetActive(!container2.activeSelf);
                    }
                }
            }
        } else
        {
            timeInd.text = "";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                if (!beatCounter)
            {
                for (int i = 1; i <= 4; i++)
                {
                    enemies[i - 1] = Instantiate(enemy, new Vector3(10 * Mathf.Pow(-1, i), 5 * i * Mathf.Pow(-1, i+ i/2 - 2*(i/4)), 40), this.transform.rotation);
                    enemies[i - 1].GetComponent<EnemyAI>().Setup(this.gameObject, this.gameObject, this.gameObject);
                }
            }
            pIn = true;
            pInBeat = beatsToSwitch;
            GetComponent<Animator>().SetBool("PlayerIn", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pIn = false;
            pInBeat = -1;
            if (!beatCounter)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Destroy(enemies[i - 1]);
                }
            }
            GetComponent<Animator>().SetBool("PlayerIn", false);
        }
    }
    public static bool isGauntlet = false;
    public void FirstLevel()
    {
        isGauntlet = container1.activeSelf;
        ChangeScene(firstLevelScene);

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}