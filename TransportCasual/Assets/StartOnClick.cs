using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartOnClick : MonoBehaviour, IPointerDownHandler
{
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (started) return;
        started = true;
        EventManager.TriggerEvent(Events.StartTap, new EventParam());
    }
}