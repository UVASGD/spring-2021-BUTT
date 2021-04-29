using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Vector2 screenBounds;
    private float width;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        width = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - width > screenBounds.x || transform.position.x + width < -screenBounds.x || transform.position.y - height > screenBounds.y || transform.position.y + height < -screenBounds.y)
        {
            Destroy(this.gameObject);
        }
    }
}
