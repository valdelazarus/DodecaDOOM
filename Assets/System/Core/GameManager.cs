using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material[] floorMats, wallMats;
    public MeshRenderer floor;
    public MeshRenderer[] walls;

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
        ChangeEnvironmentAtClockTime(clockManager.currentClockTime);
        CheckForWin();
    }

    void ChangeEnvironmentAtClockTime(int currentClockTime)
    {
        switch (currentClockTime)
        {
            case 0:
                floor.material = floorMats[0];
                foreach (MeshRenderer w in walls)
                {
                    w.material = wallMats[0];
                }
                break;
            case 3:
                floor.material = floorMats[1];
                foreach(MeshRenderer w in walls)
                {
                    w.material = wallMats[1];
                }
                break;
            case 6:
                floor.material = floorMats[2];
                foreach (MeshRenderer w in walls)
                {
                    w.material = wallMats[2];
                }
                break;
            case 9:
                floor.material = floorMats[3];
                foreach (MeshRenderer w in walls)
                {
                    w.material = wallMats[3];
                }
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

    public void GameOver()
    {
        levelManager.LoadScene("Game Over");
    }
  
}
