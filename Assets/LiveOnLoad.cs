using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LiveOnLoad : MonoBehaviour
{
    public static string name;
    private static LiveOnLoad instance = null;
    public static LiveOnLoad Instance
    {
        get { return instance; }
    }
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // any other methods you need
    private void Update()
    {

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != SceneManager.GetActiveScene().name)
            {
                print("DROPPING" + SceneManager.GetSceneAt(i).name);

                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }

        name = SceneManager.GetActiveScene().name;
    }
}
    
