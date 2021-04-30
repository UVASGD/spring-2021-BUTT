using UnityEngine;
using System.Collections;

public class LiveOnLoad : MonoBehaviour
{

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

}