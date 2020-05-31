using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSignal : MonoBehaviour
{
    public GameObject[] ghostPrefabs;

    public void SummonGhost()
    {
        int rand = Random.Range(0, ghostPrefabs.Length);
        Instantiate(ghostPrefabs[rand], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
