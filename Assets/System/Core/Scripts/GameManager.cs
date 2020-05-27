using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    public Material[] floorMats, wallMats;
    public MeshRenderer floor;
    public MeshRenderer[] walls;
    public int minWallsToDestroy, maxWallsToDestroy;
    public int gameHoursToDestroyWalls;
    public float hazardInitialDelay;

    ClockManager clockManager;
    UIManager uiManager;
    LevelManager levelManager;
    PlayerController player;
    EnvironmentalHazard hazard;

    int currentScore;

    bool paused;

    void Start()
    {
        clockManager = FindObjectOfType<ClockManager>();
        uiManager = FindObjectOfType<UIManager>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<PlayerController>();
        hazard = FindObjectOfType<EnvironmentalHazard>();

        uiManager.UpdateScoreText(currentScore);

        InvokeRepeating("DestroyRandomWalls", hazardInitialDelay + gameHoursToDestroyWalls * clockManager.incrementClockRate, gameHoursToDestroyWalls * clockManager.incrementClockRate);
    }

    void Update()
    {
        ChangeEnvironmentAtClockTime(clockManager.currentClockTime);
        CheckForWin();
        TogglePauseMenu();
    }

    void ChangeEnvironmentAtClockTime(int currentClockTime)
    {
        switch (currentClockTime)
        {
            case 0:
                floor.material = floorMats[0];
                foreach (MeshRenderer w in walls)
                {
                    if(w)
                        w.material = wallMats[0];
                }
                break;
            case 3:
                floor.material = floorMats[1];
                foreach(MeshRenderer w in walls)
                {
                    if(w)
                        w.material = wallMats[1];
                }
                break;
            case 6:
                floor.material = floorMats[2];
                foreach (MeshRenderer w in walls)
                {
                    if(w)
                        w.material = wallMats[2];
                }
                break;
            case 9:
                floor.material = floorMats[3];
                foreach (MeshRenderer w in walls)
                {
                    if(w)
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


    void TogglePauseMenu()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel")){
            paused = !paused;
            uiManager.SetActivePauseMenu(paused);
        }
        Cursor.visible = paused;
        player.enabled = !paused;

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1f;
        uiManager.SetActivePauseMenu(paused);
    }
    public void UnpauseTime()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    void DestroyRandomWalls()
    {
        if (clockManager.currentClockTime >= 12) return;

        int rand = Random.Range(minWallsToDestroy, maxWallsToDestroy + 1);
        if (rand > hazard.GetNumberOfWallsLeft())
        {
            rand--;
        }
        for (int i = 0; i < rand; ++i)
        {
            hazard.DestroyRandomWall();
        }
    }
}
