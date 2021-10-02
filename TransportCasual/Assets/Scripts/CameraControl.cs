using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private static CameraControl instance;

    private CinemachineVirtualCamera cvc;

    private void Awake()
    {
        instance = this;
        cvc = GetComponent<CinemachineVirtualCamera>();
    }

    public static void ChangeFocus(Transform target)
    {
        instance.cvc.Follow = target;
        instance.cvc.LookAt = target;
    }
}