using UnityEngine;
using UnityEngine.UI;
using PathCreation;
using TMPro;

public class LevelProgressBar : MonoBehaviour
{

    [SerializeField] private Image uiFillImage;
    [SerializeField] private TextMeshProUGUI uiStartText;
    [SerializeField] private TextMeshProUGUI uiEndText;
    [SerializeField] private int currentLevel;

     private Player player;
     private PathCreator scenePathCreator;
     private float maxDistance;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        scenePathCreator = player.pathCreator;      
        Invoke("maxDistanceBug",0f);
        SetLevelTexts(currentLevel);
    }


    public void SetLevelTexts(int currentLevel)
    {
        uiStartText.text = currentLevel.ToString();
        uiEndText.text = (currentLevel + 1).ToString();
    }

    private void maxDistanceBug()
    {
        maxDistance = GetDistance();
        Debug.LogError(maxDistance);
    }


    private float GetDistance()
    {
        return scenePathCreator.path.length - scenePathCreator.path.GetClosestDistanceAlongPath(player.transform.position);
    }


    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }


    private void Update()
    {
        float newDistance = GetDistance();
        Debug.LogError(newDistance);
        float progressValue = Mathf.InverseLerp(maxDistance, 0f, newDistance);
        UpdateProgressFill(progressValue);
    }

}