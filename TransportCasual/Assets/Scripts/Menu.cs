using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject gear;
    public GameObject settings;
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject pauseMenu;
    public GameObject pause;
    public List<GameObject> currentStatus;

    private void OnEnable()
    {
        EventManager.StartListening(Events.StartTap, StartGame);
        EventManager.StartListening(Events.LevelFinished, FinishGame);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.StartTap, StartGame);
        EventManager.StopListening(Events.LevelFinished, FinishGame);
    }

    public void StartGame(EventParam param)
    {
        currentStatus[0].SetActive(false);
        currentStatus[1].SetActive(true);
        currentStatus[2].SetActive(false);
    }

    public void FinishGame(EventParam param)
    {
        currentStatus[0].SetActive(false);
        currentStatus[1].SetActive(false);
        currentStatus[2].SetActive(true);
    }

    public void BeforeStart()
    {
        currentStatus[0].SetActive(true);
        currentStatus[1].SetActive(false);
        currentStatus[2].SetActive(false);
    }

    public void Settings()
    {
        settings.SetActive(true);
        gear.SetActive(false);
    }

    public void CloseSettings()
    {
        gear.SetActive(true);
        settings.SetActive(false);
    }

    public void RestartGame()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pause.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pause.SetActive(true);
    }

    public void NextScene()
    {
        levelManager.NextLevel();
        BeforeStart();
    }

    public void CloseSound()
    {
        //sound close
        soundOff.SetActive(false);
        soundOn.SetActive(true);
    }

    public void OpenSound()
    {
        //sound open
        soundOn.SetActive(false);
        soundOff.SetActive(true);
    }
}