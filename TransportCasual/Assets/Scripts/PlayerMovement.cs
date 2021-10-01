using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float swerveSpeed = 0.5f;
    public float maxSwerveAmount = 1f;

    private SwerveInput swerveInputHandler;
    private float swerveAmount;

    private void Awake()
    {
        swerveInputHandler = GetComponent<SwerveInput>();
    }

    private void Update()
    {
        swerveAmount = Time.deltaTime * swerveSpeed * swerveInputHandler.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(0, swerveAmount, 0);
    }
}