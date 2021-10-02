using System;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public VehicleData vehicleData;
    private PlayerMovement playerMovement;
    private FollowRoad followRoad;

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
        SusPooler.instance.SpawnFromPool("SmokePoofs", playerMovement.transform.localPosition, Quaternion.identity);
    }

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        followRoad = GetComponent<FollowRoad>();
    }

    public void SetVehicleData(VehicleData data)
    {
        vehicleData = data;
        playerMovement.maxSwerveAmount = data.swerveSpeed;
        followRoad.speed = data.moveSpeed;
    }

    private void Start()
    {
        playerMovement.maxSwerveAmount = vehicleData.swerveSpeed;
        followRoad.speed = vehicleData.moveSpeed;
    }
    
    private void Update()
    {
        
    }
}
