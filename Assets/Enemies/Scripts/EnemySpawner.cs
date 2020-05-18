using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPositions;

    public float initialDelay, repeatRate;

    void Start()
    {
        InvokeRepeating("SpawnGhosts", initialDelay, repeatRate);
    }
    public void SpawnGhosts()
    {
        int rand = Random.Range(0, spawnPositions.Length);

        Instantiate(enemyPrefab, spawnPositions[rand].position, Quaternion.identity);
    }
}
