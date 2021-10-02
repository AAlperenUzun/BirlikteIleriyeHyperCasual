using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float swipeMoveSpeed;
    private Rigidbody rb;
    private float noTouchTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }
    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
    }

    private void FixedUpdate()
    {
        StopIfNoTouch();
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        noTouchTimer = 0.1f;
        int direction = data.Direction == SwipeDirection.Left ? 1 : -1;

        Vector3 velocity = rb.velocity;
        velocity.x = direction * data.Distance / 10f * swipeMoveSpeed;
        rb.velocity = velocity;
    }

    private void StopIfNoTouch()
    {
        if (noTouchTimer >= 0f)
        {
            noTouchTimer -= Time.deltaTime;
        }
        else
        {
            Vector3 velocity = rb.velocity;
            velocity.x = 0f;
            rb.velocity = velocity;
        }
    }


}