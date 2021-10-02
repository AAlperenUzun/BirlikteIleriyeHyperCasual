using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentVehicleIndex;
    private Transform currentVehicle;

    public PathCreator pathCreator;

    private Vehicle vehicle;

    private float distanceTraveled;

    public RoadMeshCreator RoadMeshCreator { get; private set; }

    private void Awake()
    {
        if (pathCreator == null)
        {
            pathCreator = FindObjectOfType<PathCreator>();
        }

        RoadMeshCreator = pathCreator.GetComponent<RoadMeshCreator>();
        currentVehicle = transform.GetChild(currentVehicleIndex);
        vehicle = currentVehicle.GetComponent<Vehicle>();
    }

    private void Start()
    {
        RoadMeshCreator.TriggerUpdate();
    }

    private void Update()
    {
        distanceTraveled += vehicle.speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
    }

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
        currentVehicle.gameObject.SetActive(false);
        currentVehicleIndex++;
        currentVehicle = transform.GetChild(currentVehicleIndex);
        currentVehicle.gameObject.SetActive(true);
        vehicle = currentVehicle.GetComponent<Vehicle>();
    }
}