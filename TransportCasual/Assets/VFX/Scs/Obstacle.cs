using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Obstacle : MonoBehaviour
{
    public int Amount;    
    [Header("Set if moving object")]
    public bool movingObstacle;
    public int moveSpeed;

    private bool started;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        EventManager.StartListening(Events.StartTap, onScreenTapped);
    }
    private void onCollision()
    {
        var eventParam = new EventParam { intParam = Amount };
        EventManager.TriggerEvent(Events.MoneyCollect, eventParam);
        CameraControl.instance.ShakeCamera(20f, .2f);
    }

    private void onScreenTapped(EventParam param)
    {
        started = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        onCollision();
    }

    private void FixedUpdate()
    {
        if (movingObstacle && started)
        {
            rb.velocity = transform.right * moveSpeed;
        }
    }
}
