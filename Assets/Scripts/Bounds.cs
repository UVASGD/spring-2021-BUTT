using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
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
    void LateUpdate()
    {
        Vector3 playerpos = transform.position;
        playerpos.x = Mathf.Clamp(playerpos.x, screenBounds.x * -1 + width, screenBounds.x - width);
        playerpos.y = Mathf.Clamp(playerpos.y, -3.45F + height, screenBounds.y - height);
        transform.position = playerpos;
    }
}
