using System;
using Unity.Mathematics;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Speed variables")]
    public float speed;
    public float maxSwerve;

    
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
        var poof = SusPooler.instance.SpawnFromPool("SmokePoofs", transform.position, Quaternion.identity);
        //poof.transform.parent = playerMovement.transform;
    }

}