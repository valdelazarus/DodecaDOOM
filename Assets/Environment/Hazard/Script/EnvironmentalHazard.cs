using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalHazard : MonoBehaviour
{
    public Wall[] walls;
    public GameObject explosionFX;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DestroyRandomWall()
    {
        int rand;
        do
        {
            rand = Random.Range(0, walls.Length);
        } while (walls[rand] == null);

        Wall targettedWall = walls[rand];
        walls[rand] = null;

        foreach (GameObject w in targettedWall.wallBlocks)
        {
            Instantiate(explosionFX, w.transform.position + Vector3.up * .1f, Quaternion.identity);
            Destroy(w,1f);
        }
        Destroy(targettedWall.shadowObj, 1f);
        audioSource.Play();
    }

    public int GetNumberOfWallsLeft()
    {
        int wallsLeft = 0;
        foreach(Wall w in walls)
        {
            if (w != null)
            {
                wallsLeft++;
            }
        }
        return wallsLeft;
    }
}
[System.Serializable]
public class Wall
{
    public GameObject[] wallBlocks;
    public GameObject shadowObj;
}
