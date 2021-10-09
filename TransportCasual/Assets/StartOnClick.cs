using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartOnClick : MonoBehaviour, IPointerDownHandler
{
    public bool started;

    private void OnEnable()
    {
        EventManager.StartListening(Events.LevelFinished, ResetTap);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.LevelFinished, ResetTap);
    }

    private void ResetTap(EventParam param)
    {
        started = false;
    }

    private void ResetTap2()
    {
        started = false;
    }

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
        Invoke(nameof(ResetTap2), 1f);
    }
}