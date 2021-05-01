using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LiveOnLoad2 : MonoBehaviour { 

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
 
}

