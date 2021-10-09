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
    private bool started;

    public GameObject objectToActivate;

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
        distanceTraveled += vehicle.speed * Time.deltaTime * 10;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
    }

    private float GetDistance()
    {
        return pathCreator.path.length - pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    private void FixedUpdate()
    {
        if (!started) return;
        distanceTraveled += vehicle.speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop);

        if (GetDistance() < .1f)
        {
            EventManager.TriggerEvent(Events.LevelFinished, new EventParam());
            started = false;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening(Events.VehicleChange, OnVehicleChange);
        EventManager.StartListening(Events.StartTap, OnTap);
        EventManager.StartListening(Events.LevelWon, ActivateObject);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.VehicleChange, OnVehicleChange);
        EventManager.StopListening(Events.StartTap, OnTap);
        EventManager.StopListening(Events.LevelWon, ActivateObject);

    }

    private void ActivateObject(EventParam param)
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        SusPooler.instance.SpawnFromPool("SmokePoofs", transform.position, Quaternion.identity);
    }

    private void OnVehicleChange(EventParam param)
    {
        currentVehicle.gameObject.SetActive(false);
        var oldMovement = currentVehicle.GetComponentInChildren<PlayerMovement>();

        if (param.isDowngrade)
        {
            currentVehicleIndex--;
            currentVehicleIndex = Mathf.Clamp(currentVehicleIndex, 0, 2);
        }
        else
        {
            currentVehicleIndex++;
            currentVehicleIndex = Mathf.Clamp(currentVehicleIndex, 0, 2);
        }

        currentVehicle = transform.GetChild(currentVehicleIndex);
        var newMovement = currentVehicle.GetComponentInChildren<PlayerMovement>();
        newMovement.Started = true;
        newMovement.transform.position = oldMovement.transform.position;
        currentVehicle.gameObject.SetActive(true);
        vehicle = currentVehicle.GetComponent<Vehicle>();
    }

    private void OnTap(EventParam param)
    {
        started = true;
    }
}