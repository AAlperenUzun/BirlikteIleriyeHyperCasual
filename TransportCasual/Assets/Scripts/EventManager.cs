using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Events
{
    StartTap,
    MoneyCollect,
    VehicleChange,
    LevelFinished,
    LevelWon,
    LevelLost
}

public class EventManager : MonoBehaviour
{

    private Dictionary<Events, Action<EventParam>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<Events, Action<EventParam>>();
        }
    }

    public static void StartListening(Events eventName, Action<EventParam> listener)
    {
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] += listener;
        }
        else
        {
            instance.eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(Events eventName, Action<EventParam> listener)
    {
        if (instance.eventDictionary.ContainsKey(eventName))
        {
            instance.eventDictionary[eventName] -= listener;
        }
    }

    public static void TriggerEvent(Events eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }
}

//Re-usable structure/ Can be a class to. Add all parameters you need inside it
public struct EventParam
{
    public string param1;
    public int intParam;
    public float param3;
    public bool isDowngrade;
}