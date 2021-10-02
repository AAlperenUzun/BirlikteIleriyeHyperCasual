using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    private int currentVehicle;

    private void OnEnable()
    {
        EventManager.StartListening(Events.VehicleChange, OnVehicleChange);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.VehicleChange, OnVehicleChange);
    }

    private void OnVehicleChange(EventParam param)
    {
        transform.GetChild(currentVehicle).gameObject.SetActive(false);
        currentVehicle++;
        transform.GetChild(currentVehicle).gameObject.SetActive(true);
    }
}