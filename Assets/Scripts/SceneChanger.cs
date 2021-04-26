using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string firstLevelScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    /**
     * Called from the starting menu to load the first level scene of the game.
     * Set the firstLevelScene variable to the scene's name to configure which scene gets loaded.
     */
    public void FirstLevel()
    {
        ChangeScene(firstLevelScene);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChangeScene(firstLevelScene);
        }
    }
}