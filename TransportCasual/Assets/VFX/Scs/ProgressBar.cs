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
        EventManager.StartListening(Events.LevelFinished, OnFinish);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.MoneyCollect, OnCollect);
        EventManager.StopListening(Events.LevelFinished, OnFinish);
    }

    private void OnCollect(EventParam param)
    {
        changeProgress(param.intParam);
    }

    private void OnFinish(EventParam param)
    {
        if (startProgress < maxProgress)
        {
            EventManager.TriggerEvent(Events.LevelWon, new EventParam()); //lost
        }
        else
        {
            EventManager.TriggerEvent(Events.LevelWon, new EventParam());
        }
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
        else if (lastValue > finalValue)
        {
            CheckDowngradeCar(lastValue, finalValue);
        }
    }

    private void CheckDowngradeCar(float lastValue, float finalValue)
    {
        if (lastValue > maxProgress / 2 && finalValue < maxProgress / 2)
        {
            var obj = SusPooler.instance.SpawnFromPool("badMilestonePoofs", frontBar.transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(-50, 700, 0);
            EventManager.TriggerEvent(Events.VehicleChange, new EventParam { isDowngrade = true });
        }
        else if (lastValue == maxProgress)
        {
            var obj = SusPooler.instance.SpawnFromPool("badMilestonePoofs", frontBar.transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(-50, 700, 0);
            EventManager.TriggerEvent(Events.VehicleChange, new EventParam { isDowngrade = true });
        }
    }

    private void setParticles(float finalValue, string objectName)
    {
        if (finalValue == 0)
        {
            var obj = SusPooler.instance.SpawnFromPool(objectName, frontBar.transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(-50, 700, 0);
        }
        else if (finalValue == maxProgress / 2)
        {
            var obj = SusPooler.instance.SpawnFromPool(objectName, frontBar.transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(-50, 700, 0);
            EventManager.TriggerEvent(Events.VehicleChange, new EventParam { isDowngrade = false });
        }
        else if (finalValue == maxProgress)
        {
            var obj = SusPooler.instance.SpawnFromPool(objectName, frontBar.transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(-50, 700, 0);
            EventManager.TriggerEvent(Events.VehicleChange, new EventParam { isDowngrade = false });
        }
    }

    public void changeProgress(float amount)
    {
        currentProgress = Mathf.Clamp(progress + amount, 0, maxProgress);
        progress = Mathf.Clamp(progress + amount, 0, maxProgress);
        lerpTimer = 0f;
    }
}