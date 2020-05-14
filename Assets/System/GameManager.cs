using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] environmentGroups;

    ClockManager clockManager;

    void Start()
    {
        clockManager = FindObjectOfType<ClockManager>();    
    }

    
    void Update()
    {
        EnableEnvironmentAtClockTime(clockManager.currentClockTime);
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
}
