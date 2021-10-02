using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class LevelManager : MonoBehaviour
{
    public List<Transform> levels;
    private Transform currentLevel;
    [NonSerialized]public int currentLevelNumber;
    PlayerData loadData;
    private void Awake()
    {
        currentLevelNumber = 0;
        loadData = LoadPlayer();
        if (loadData!=null)
        {
            currentLevelNumber = loadData.currentLevelNumber;
        }
        GetCurrentLevel();
        //NextLevel();
    }
    private void GetCurrentLevel()
    {
        if (levels.Count < currentLevelNumber)
        {
            currentLevelNumber = 0;
        }
        currentLevel = Instantiate(levels[currentLevelNumber], transform);
    }
    private void NextLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevelNumber += 1;
        SaveLevel();
        GetCurrentLevel();
    }
    public void SaveLevel()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.level";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(this);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.level";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            
            return data;
        }
        else
        {
            return null;
        }
    }
}
