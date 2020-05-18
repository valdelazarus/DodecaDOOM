using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] environmentGroups;

    ClockManager clockManager;
    UIManager uiManager;
    LevelManager levelManager;
    int currentScore;

    void Start()
    {
        clockManager = FindObjectOfType<ClockManager>();
        uiManager = FindObjectOfType<UIManager>();
        levelManager = FindObjectOfType<LevelManager>();
        uiManager.UpdateScoreText(currentScore);
    }

    void Update()
    {
        EnableEnvironmentAtClockTime(clockManager.currentClockTime);
        CheckForWin();
    }

    void EnableEnvironmentAtClockTime(int currentClockTime)
    {
        switch (currentClockTime)
        {
            case 3:
                environmentGroups[0].SetActive(false);
                environmentGroups[1].SetActive(true);
                break;
            case 6:
                environmentGroups[1].SetActive(false);
                environmentGroups[2].SetActive(true);
                break;
            case 9:
                environmentGroups[2].SetActive(false);
                environmentGroups[3].SetActive(true);
                break;
        }
    }

    public void AddScore(int value)
    {
        currentScore += value;
        PlayerPrefs.SetInt("Score", currentScore);
        uiManager.UpdateScoreText(currentScore);
    }

    void CheckForWin()
    {
        //to be replaced later with proper win condition
        if (clockManager.currentClockTime > 12)
        {
            levelManager.LoadScene("Game Win");
        }
    }

  
}
