using System;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class FollowRoad : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    public float speed = 5;
    private float distanceTraveled;
    
    public RoadMeshCreator RoadMeshCreator { get; private set; }

    private void Awake()
    {
        RoadMeshCreator = pathCreator.GetComponent<RoadMeshCreator>();
    }

    private void Start()
    {
        RoadMeshCreator.TriggerUpdate();
    }

    private void Update()
    {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
    }

}
