using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTravel : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.25)
            GetComponent<CircleCollider2D>().enabled = true;
        if (timer <= 0.0)
            Delete();
        timer -= Time.deltaTime;
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }
}
