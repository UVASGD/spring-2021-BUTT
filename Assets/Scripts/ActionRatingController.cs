using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRatingController : MonoBehaviour
{
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

        GameObject arIndicator = Instantiate(arIndicatorPrefab, this.transform);
        arIndicator.SendMessage("ShowActionRating", ar);
    }
}
