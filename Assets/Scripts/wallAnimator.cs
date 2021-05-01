using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallAnimator : MonoBehaviour
{
    Animator animator;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", speed);
    }
}
