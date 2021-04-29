using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Balloon : MonoBehaviour
{
    public MusicManager mm;
    LineRenderer head;
    Rigidbody2D rb;
    public LineRenderer line;
    public GameObject boomBoomZone;
    Shoot shootScript;
    public float inRadius = .42F, outRadius = .8F;
    public float minDisplayMagnitude= .25F;
    public float force = 500;
    float curPercent = 100;
    public float percentIncrement = .01F;
    float origForce, origGrav;
    float scaleOverTime = .125F;
    public TextMeshPro diffInd;
    // Start is called before the first frame update
    void Start()
    {
        shootScript = GetComponent<Shoot>();
        head = GetComponent<LineRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        origForce = force;
        origGrav = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        line.enabled = true;
        head.enabled = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 vec2Mouse = mousePos - transform.position;
        vec2Mouse.z = 0;
        mousePos.z = 6;
        vec2Mouse.Normalize();
        Vector3 lineStart = transform.position + vec2Mouse * outRadius;
        lineStart.z = 0;

        line.SetPosition(0, lineStart + Vector3.forward * 6);
        line.SetPosition(1, mousePos);

        head.SetPosition(0, vec2Mouse * inRadius + Vector3.forward * 6);
        head.SetPosition(1, vec2Mouse * outRadius + Vector3.forward * 6);
        if (Input.GetMouseButtonDown(0))
        {
            ActionRating ar = mm.RateAction();
            if (ar != ActionRating.INVALID)
            {
                rb.AddForce(-force * vec2Mouse, ForceMode2D.Impulse);
            }
            float actionScore = (3F - ((float)ar % 4))/3F;
            actionScore += scaleOverTime;
            rb.gravityScale += percentIncrement * actionScore  * origGrav;
            force += percentIncrement * actionScore * origForce;
            curPercent+= (actionScore*4);
            diffInd.text = "Weight: " + (int)curPercent + "%";
        }
        if (Input.GetMouseButtonDown(1))
        {
            ActionRating ar = mm.RateAction();
            if (ar != ActionRating.INVALID)
            {
                Instantiate(boomBoomZone, transform.position - Vector3.forward, transform.rotation, transform);
            }
            
        }
       
        
    }

}
