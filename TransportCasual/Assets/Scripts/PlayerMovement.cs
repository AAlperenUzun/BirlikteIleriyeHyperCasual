using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float swipeMoveSpeed;

    private float noTouchTimer;
    private Rigidbody rb;

    public float roadEdgeMargin = .5f;

    private float roadWidth;
    private float RoadMin => RoadMax * -1;
    private float RoadMax => roadWidth - 0.5f;
    private bool started;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        roadWidth = transform.parent.parent.GetComponent<Player>().RoadMeshCreator.roadWidth;
    }

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        EventManager.StartListening(Events.StartTap, OnTap);
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
        EventManager.StopListening(Events.StartTap, OnTap);
    }

    private void OnTap(EventParam param)
    {
        started = true;
    }

    private void FixedUpdate()
    {
        StopIfNoTouch();
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (!started) return;
        
        noTouchTimer = 0.1f;
        int direction = data.Direction == SwipeDirection.Left ? -1 : 1;

        if (direction > 0 && (RoadMax - transform.localPosition.y) < roadEdgeMargin) return;
        if (direction < 0 && (transform.localPosition.y - RoadMin) < roadEdgeMargin) return;
        Debug.Log("move");
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