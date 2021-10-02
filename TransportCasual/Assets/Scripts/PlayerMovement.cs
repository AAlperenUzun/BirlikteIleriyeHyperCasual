using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float roadEdgeMargin = .1f;

    private const float swerveSpeed = 0.5f;
    private SwerveInput swerveInputHandler;
    private Vehicle vehicle;
    private float swerveAmount;

    private float roadWidth;
    private float RoadMin => RoadMax * -1;
    private float RoadMax => roadWidth - 0.5f;
    

    private void Awake()
    {
        swerveInputHandler = GetComponent<SwerveInput>();
        vehicle = GetComponentInParent<Vehicle>();
    }

    private void Start()
    {
        roadWidth = transform.parent.parent.GetComponent<Player>().RoadMeshCreator.roadWidth;
    }

    private void Update()
    {
        swerveAmount = Time.deltaTime * swerveSpeed * swerveInputHandler.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -vehicle.maxSwerve, vehicle.maxSwerve);
        TranslateCheck(swerveAmount);
    }

    private void TranslateCheck(float swerve)
    {
        if (swerve > 0 && (RoadMax - transform.localPosition.y) < roadEdgeMargin) return;
        if (swerve < 0 && (transform.localPosition.y - RoadMin) < roadEdgeMargin) return;
        transform.Translate(0, swerve, 0);
    }
}