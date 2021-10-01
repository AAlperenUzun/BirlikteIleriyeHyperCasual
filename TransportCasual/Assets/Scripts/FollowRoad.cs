using System;
using PathCreation;
using UnityEngine;

public class FollowRoad : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    public float speed = 5;
    private float distanceTraveled;
    
    private void Update()
    {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
    }

}
