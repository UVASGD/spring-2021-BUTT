using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRatingIndicator : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowActionRating(ActionRating ar)
    {
        print(ar);
        sr.sprite = sprites[(int)ar];
    }
}
