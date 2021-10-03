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
    private void onCollision(Collider other)
    {
        var eventParam = new EventParam { intParam = Amount };
        EventManager.TriggerEvent(Events.MoneyCollect, eventParam);

        CameraControl.instance.ShakeCamera(20f, .2f);
        rb.useGravity = true;
        rb.AddExplosionForce(500f, ((other.transform.position + transform.position) / 2) + new Vector3(0,-2f,0), 20f);
        
    }

    private void onScreenTapped(EventParam param)
    {
        started = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        onCollision(other);
    }

    private void FixedUpdate()
    {
        if (movingObstacle && started)
        {
            rb.velocity = transform.right * moveSpeed;
        }
    }
}
