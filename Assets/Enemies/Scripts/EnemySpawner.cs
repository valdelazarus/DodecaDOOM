using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;

    public Transform[] spawnPositions;
    public Transform[] bossSpawnPositions;
    
    public float initialDelay, repeatRate;

    ClockManager clockManager;
    UIManager uiManager;
    bool bossSpawned;

    void Start()
    {
        clockManager = FindObjectOfType<ClockManager>();
        uiManager = FindObjectOfType<UIManager>();

        InvokeRepeating("SpawnGhosts", initialDelay, repeatRate);
        InvokeRepeating("CheckToSpawnBoss", 0f, clockManager.incrementClockRate);
    }
    public void SpawnGhosts()
    {
        int rand = Random.Range(0, spawnPositions.Length);
        int enemyRand = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enemyRand], spawnPositions[rand].position, Quaternion.identity);
    }
    void CheckToSpawnBoss()
    {
        SpawnBoss(clockManager.currentClockTime);
    }
    void SpawnBoss(int currentClockTime)
    {
        int rand;
        switch (currentClockTime)
        {
            case 1:
            case 2:
                bossSpawned = false;
                break;
            case 3:
                if (!bossSpawned)
                {
                    bossSpawned = true;
                    rand = Random.Range(0, bossSpawnPositions.Length);
                    Instantiate(bossPrefabs[0], bossSpawnPositions[rand].position, Quaternion.identity);
                    uiManager.DisplayBossInfo("Baby");
                }
                break;
            case 4:
            case 5:
                bossSpawned = false;
                break;
            case 6:
                if (!bossSpawned)
                {
                    bossSpawned = true;
                    rand = Random.Range(0, bossSpawnPositions.Length);
                    Instantiate(bossPrefabs[1], bossSpawnPositions[rand].position, Quaternion.identity);
                    uiManager.DisplayBossInfo("Teen");
                }
                break;
            case 7:
            case 8:
                bossSpawned = false;
                break;
            case 9:
                if (!bossSpawned)
                {
                    bossSpawned = true;
                    rand = Random.Range(0, bossSpawnPositions.Length);
                    Instantiate(bossPrefabs[2], bossSpawnPositions[rand].position, Quaternion.identity);
                    uiManager.DisplayBossInfo("Adult");
                }
                break;
            case 10:
            case 11:
                bossSpawned = false;
                break;
            case 12:
                if (!bossSpawned)
                {
                    bossSpawned = true;
                    rand = Random.Range(0, bossSpawnPositions.Length);
                    Instantiate(bossPrefabs[2], bossSpawnPositions[rand].position, Quaternion.identity);
                    uiManager.DisplayBossInfo("Final");
                }
                break;
        }
    }
}
