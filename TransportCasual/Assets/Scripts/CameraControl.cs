using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startIntensity;
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
    


    public void ShakeCamera(float intensity, float timer)
    {

        CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _shakeTimerTotal = timer;
        _shakeTimer = timer;
        _startIntensity = intensity;
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_startIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
        }
    }
}
