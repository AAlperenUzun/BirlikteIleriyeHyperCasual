using UnityEngine;
using UnityEngine.UI;
using PathCreation;
using TMPro;

public class LevelProgressBar : MonoBehaviour
{

    [SerializeField] private Image uiFillImage;
    [SerializeField] private TextMeshProUGUI uiStartText;
    [SerializeField] private TextMeshProUGUI uiEndText;

     private Player player;
     private PathCreator scenePathCreator;
     private float maxDistance;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        scenePathCreator = player.pathCreator;

        maxDistance = GetDistance();
    }


    public void SetLevelTexts(int currentLevel)
    {
        uiStartText.text = currentLevel.ToString();
        uiEndText.text = (currentLevel + 1).ToString();
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
        // check if the player doesn't pass the End Line

        if (GetDistance()>=1f)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(maxDistance, 0f, newDistance);

            UpdateProgressFill(progressValue);
        }
    }

}