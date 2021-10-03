using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float swipeMoveSpeed;
    private float noTouchTimer;
    private Rigidbody rb;

    public float roadEdgeMargin = .1f;

    private float roadWidth;
    private float RoadMin => RoadMax * -1;
    private float RoadMax => roadWidth - 0.5f;

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
        int direction = data.Direction == SwipeDirection.Left ? -1 : 1;

        /*if (direction > 0 && (RoadMax - transform.localPosition.y) < roadEdgeMargin) return;
        if (direction < 0 && (transform.localPosition.y - RoadMin) < roadEdgeMargin) return;
        */

        transform.Translate(0, direction * data.Distance / 10f * swipeMoveSpeed * Time.deltaTime, 0);
        

         /*var locVel = transform.InverseTransformDirection(rb.velocity);
         locVel.y = direction * data.Distance / 10f * swipeMoveSpeed;
         rb.velocity = transform.TransformDirection(locVel);
         */
        
    }

    private void StopIfNoTouch()
    {
        if (noTouchTimer >= 0f)
        {
            noTouchTimer -= Time.deltaTime;
        }
        else
        {
            /*var locVel = transform.InverseTransformDirection(rb.velocity);
            locVel.y = 0f;
            rb.velocity = transform.TransformDirection(locVel);
            */
        }
    }


}