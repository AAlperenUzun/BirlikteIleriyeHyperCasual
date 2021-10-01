using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public VehicleData vehicleData;
    private PlayerMovement playerMovement;
    private FollowRoad followRoad;

    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        followRoad = GetComponent<FollowRoad>();
    }

    public void SetVehicleData(VehicleData data)
    {
        vehicleData = data;
        playerMovement.swerveSpeed = data.swerveSpeed;
        followRoad.speed = data.moveSpeed;
    }

    private void Start()
    {
        playerMovement.swerveSpeed = vehicleData.swerveSpeed;
        followRoad.speed = vehicleData.moveSpeed;
    }
    
    private void Update()
    {
        
    }
}
