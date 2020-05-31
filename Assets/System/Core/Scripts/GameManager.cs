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
    public float revivingDuration;

    ClockManager clockManager;
    UIManager uiManager;
    LevelManager levelManager;
    GameObject player;
    EnvironmentalHazard hazard;

    int currentScore;
    int savingGraceCount;

    bool paused;
    bool playerDead;
    bool reviving;
    bool wallDestroyed;

    void Start()
    {
        clockManager = FindObjectOfType<ClockManager>();
        uiManager = FindObjectOfType<UIManager>();
        levelManager = FindObjectOfType<LevelManager>();
        hazard = FindObjectOfType<EnvironmentalHazard>();

        player = GameObject.FindWithTag("Player");

        uiManager.UpdateScoreText(currentScore);
        uiManager.UpdateSavingGraceCountText(savingGraceCount);
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
                    if (w)
                        w.material = wallMats[0];
                }
                wallDestroyed = false;
                break;
            case 3:
                floor.material = floorMats[1];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[1];
                }
                if (!wallDestroyed)
                {
                    wallDestroyed = true;
                    DestroyRandomWalls();
                }
                break;
            case 4:
            case 5:
                floor.material = floorMats[1];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[1];
                }
                wallDestroyed = false;
                break;
            case 6:
                floor.material = floorMats[2];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[2];
                }
                if (!wallDestroyed)
                {
                    wallDestroyed = true;
                    DestroyRandomWalls();
                }
                break;
            case 7:
            case 8:
                floor.material = floorMats[2];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[2];
                }
                wallDestroyed = false;
                break;
            case 9:
                floor.material = floorMats[3];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[3];
                }
                if (!wallDestroyed)
                {
                    wallDestroyed = true;
                    DestroyRandomWalls();
                }
                break;
            case 10:
            case 11:
                floor.material = floorMats[3];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[3];
                }
                wallDestroyed = false;
                break;
            case 12:
                floor.material = floorMats[3];
                foreach (MeshRenderer w in walls)
                {
                    if (w)
                        w.material = wallMats[3];
                }
                if (!wallDestroyed)
                {
                    wallDestroyed = true;
                    DestroyRandomWalls();
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
    }

    void DeductSavingGrace()
    {
        savingGraceCount--;
        uiManager.UpdateSavingGraceCountText(savingGraceCount);
    }

    void SignalRevivingPlayer()
    {
        player.GetComponent<PlayerSound>().PlaySoundEffect(PlayerSound.EffectType.REVIVED);
        uiManager.ShowRevivingIndicator();
        Invoke("RevivePlayerAtPreviousHour", revivingDuration);
    }

    void RevivePlayerAtPreviousHour()
    {
        uiManager.HideRevivingIndicator();
        uiManager.HideChallengeCountdownPanel();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }

        clockManager.SetClockTime(clockManager.currentClockTime - 1);
        clockManager.ResetCountdown();

        playerDead = false;
        player.GetComponent<Health>().ResetHealth();
        player.transform.position = Vector3.zero;
        player.GetComponent<Animator>().SetTrigger("revive");
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Collider>().enabled = true;
        reviving = false;
    }

    public void CheckToRevive()
    {
        if (reviving) return;
        playerDead = true;
        if (savingGraceCount != 0)
        {
            reviving = true;
            DeductSavingGrace();
            SignalRevivingPlayer();
        }
        else
        {
            GameOver();
        }
    }
}
