using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int Amount;
    
    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnCollect()
    {
        var eventParam = new EventParam {intParam = Amount};
        EventManager.TriggerEvent(Events.MoneyCollect, eventParam);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollect();
    }
}