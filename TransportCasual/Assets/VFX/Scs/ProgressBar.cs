using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private float startProgress;

    [SerializeField]
    private float maxProgress, chipSpeed;

    [SerializeField]
    private Image frontBar, backBar;

    private float lerpTimer;

    private float currentProgress
    {
        set { checkMilestone(startProgress, value); }
    }

    public float progress
    {
        get { return startProgress; }
        set { startProgress = value; }
    }

    private void OnEnable()
    {
        EventManager.StartListening(Events.MoneyCollect, OnCollect);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.MoneyCollect, OnCollect);
    }

    private void OnCollect(EventParam param)
    {
        changeProgress(param.intParam);
    }

    // Start is called before the first frame update
    void Start()
    {
        frontBar.fillAmount = progress / maxProgress;
    }

    // Update is called once per frame
    void Update()
    {
        progress = Mathf.Clamp(progress, 0, maxProgress);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.A))
        {
            changeProgress(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            changeProgress(-1);
        }
    }

    private void UpdateHealthUI()
    {
        float fillF = frontBar.fillAmount;
        float fillB = backBar.fillAmount;
        float fraction = startProgress / maxProgress;

        if (fillB > fraction)
        {
            frontBar.fillAmount = fraction;
            backBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            //percentComplete *= percentComplete;               
            backBar.fillAmount = Mathf.Lerp(fillB, fraction, percentComplete);
        }
        else
        {
            backBar.fillAmount = fraction;
            backBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            //percentComplete *= percentComplete;
            frontBar.fillAmount = Mathf.Lerp(fillF, backBar.fillAmount, percentComplete);
        }
    }

    private void checkMilestone(float lastValue, float finalValue)
    {
        if (lastValue < finalValue)
        {
            setParticles(finalValue, "MilestonePoofs");
        }
        else
        {
            setParticles(finalValue, "badMilestonePoofs");
        }
    }

    private void setParticles(float finalValue, string objectName)
    {
        if (finalValue == 0)
        {
            SusPooler.instance.SpawnFromPool(objectName, frontBar.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        }
        else if (finalValue == maxProgress / 2)
        {
            SusPooler.instance.SpawnFromPool(objectName, frontBar.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            EventManager.TriggerEvent(Events.VehicleChange, new EventParam());
        }
        else if (finalValue == maxProgress)
        {
            SusPooler.instance.SpawnFromPool(objectName, frontBar.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            EventManager.TriggerEvent(Events.LevelFinished, new EventParam());
        }
    }

    public void changeProgress(float amount)
    {
        currentProgress = progress + amount;
        progress += amount;
        lerpTimer = 0f;
    }
}