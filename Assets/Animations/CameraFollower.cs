using System;
using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    private Transform target;
    public float damping = 0.3f;
    public float lookAheadFactor = 0;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAPos;


    // Use this for initialization
    private void Start()
    {
        target = GameObject.Find("Player").transform;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAPos = Vector3.MoveTowards(m_LookAPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

        m_LastTargetPosition = target.position;
    }
}
