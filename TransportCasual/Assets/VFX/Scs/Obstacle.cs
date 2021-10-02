using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int Amount;
    private void onCollision()
    {
        var eventParam = new EventParam { intParam = Amount };
        EventManager.TriggerEvent(Events.MoneyCollect, eventParam);
        //Collision particle effect
    }

    private void OnTriggerEnter(Collider other)
    {
        onCollision();
    }
}
