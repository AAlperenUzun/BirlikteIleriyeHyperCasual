using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevelNumber;
    public PlayerData(LevelManager levelManager)
    {
        currentLevelNumber = levelManager.currentLevelNumber;
    }
}
