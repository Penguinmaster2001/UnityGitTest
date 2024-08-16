using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    public Vector3 moveAmount = Vector3.zero;
    public float smoothTime = 0.2f;

    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private bool movingTowardsTarget = true;

    void Start()
    {
        startingPosition = transform.position;
        targetPosition = startingPosition + moveAmount;
    }

    void Update()
    {
        if (movingTowardsTarget)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            if ((transform.position - targetPosition).sqrMagnitude < 0.5f)
            {
                movingTowardsTarget = false;
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, startingPosition, ref velocity, smoothTime);
            if ((transform.position - startingPosition).sqrMagnitude < 0.5f)
            {
                movingTowardsTarget = true;
            }
        }
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
