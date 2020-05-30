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
    GameObject player;
    EnvironmentalHazard hazard;

    int currentScore;
    int savingGraceCount;
    int currentClockTime;

    bool paused;
    bool playerDead;

    void Start()
    {
        clockManager = FindObjectOfType<ClockManager>();
        uiManager = FindObjectOfType<UIManager>();
        levelManager = FindObjectOfType<LevelManager>();
        hazard = FindObjectOfType<EnvironmentalHazard>();

        player = GameObject.FindWithTag("Player");

        if (PlayerPrefs.HasKey("Score"))
        {
            currentScore = PlayerPrefs.GetInt("Score");
        }
        if (PlayerPrefs.HasKey("SavingGrace"))
        {
            savingGraceCount = PlayerPrefs.GetInt("SavingGrace");
        }
        if (PlayerPrefs.HasKey("ClockTime"))
        {
            currentClockTime = PlayerPrefs.GetInt("ClockTime");
        }

        uiManager.UpdateScoreText(currentScore);
        uiManager.UpdateSavingGraceCountText(savingGraceCount);
        clockManager.SetClockTime(currentClockTime);

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
            case 1:
            case 2:
                floor.material = floorMats[0];
                foreach (MeshRenderer w in walls)
                {
                    if(w)
                        w.material = wallMats[0];
                }
                break;
            case 3:
            case 4:
            case 5:
                floor.material = floorMats[1];
                foreach(MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[1];
                }
                break;
            case 6:
            case 7:
            case 8:
                floor.material = floorMats[2];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[2];
                }
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                floor.material = floorMats[3];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
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
        if (clockManager.currentClockTime > 12)
        {
            if (FindObjectsOfType<BossHealthBar>().Length == 0)
            {
                levelManager.LoadScene("Game Win");
            }
            else
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        levelManager.LoadScene("Game Over");
    }


    void TogglePauseMenu()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel")){
            paused = !paused;
            uiManager.SetActivePauseMenu(paused);
            if (!paused)
            {
                uiManager.HideRules();
            }
        }
        Cursor.visible = paused;

        if (playerDead)
        {
            player.GetComponent<PlayerController>().enabled = false;
        } else
        {
            player.GetComponent<PlayerController>().enabled = !paused;
        }
         
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

    public void AddSavingGrace()
    {
        savingGraceCount++;
        uiManager.UpdateSavingGraceCountText(savingGraceCount);
        PlayerPrefs.SetInt("SavingGrace", savingGraceCount);
    }

    void DeductSavingGrace()
    {
        savingGraceCount--;
        uiManager.UpdateSavingGraceCountText(savingGraceCount);
        PlayerPrefs.SetInt("SavingGrace", savingGraceCount);
    }

    void RevivePlayerAtPreviousHour()
    {
        player.GetComponent<PlayerSound>().PlaySoundEffect(PlayerSound.EffectType.REVIVED);
        PlayerPrefs.SetInt("ClockTime", clockManager.currentClockTime - 1);
        uiManager.ShowRevivingIndicator();
        levelManager.LoadScene("Game");
    }

    public void CheckToRevive()
    {
        playerDead = true;
        if (savingGraceCount != 0)
        {
            DeductSavingGrace();
            RevivePlayerAtPreviousHour();
        }
        else
        {
            GameOver();
        }
    }
}
