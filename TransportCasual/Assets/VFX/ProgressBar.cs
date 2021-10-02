using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ProgressBar : MonoBehaviour
{
    private float lerpTimer;
    [SerializeField] private float startProgress;
    [SerializeField] private float maxProgress, chipSpeed;
    [SerializeField] private Image frontBar, backBar;
    [SerializeField] private VisualEffect milestoneHitEffect;

    public float progress
    {
        get { return startProgress; }
        set {
            checkMilestone(startProgress, value);
            startProgress = value;
            
        }
    }

    private List<float> milestones = new List<float>();
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
            setParticles(lastValue, milestoneHitEffect);
        }
        else
        {

        }
    }

    private void setParticles(float lastValue, VisualEffect effect)
    {
        if (lastValue == 0)
        {

        }
        else if (lastValue == progress)
        {

        }
        else if (lastValue == progress * 2)
        {

        }
    }

    public void changeProgress(float amount)
    {
        progress += amount;
        lerpTimer = 0f;
    }
}
