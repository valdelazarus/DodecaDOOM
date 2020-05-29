using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject savingGracePrefab;
    public float dropRate;
    bool called; 
    
    public void SpawnPickup()
    {
        if (called) return;
        called = true;
        float rand = Random.value;
        if (rand <= dropRate)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 0f, transform.position.z);
            Instantiate(savingGracePrefab, spawnPos, Quaternion.identity);
        }
    }
}
