using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRatingController : MonoBehaviour
{
    bool black = false;
    public GameObject arIndicatorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowActionRating(ActionRating ar)
    {

        GameObject arIndicator = Instantiate(arIndicatorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        arIndicator.SendMessage("ShowActionRating", ar);
    }
}
