using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public float incrementClockRate; // every ? second(s)

    public int currentClockTime = 0; // 0 - 12

    UIManager uiManager;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        uiManager = FindObjectOfType<UIManager>();

        uiManager.UpdateClockText(currentClockTime.ToString());

        //increment clock time every 1 minute in game time
        InvokeRepeating("IncrementClockTime", incrementClockRate, incrementClockRate);
    }

    void IncrementClockTime()
    {
        audioSource.Play();
        if (currentClockTime > 12) return;
        currentClockTime++;
        if (currentClockTime <= 12)
            uiManager.UpdateClockText(currentClockTime.ToString());
    }

    public void SetClockTime(int clockTime)
    {
        if (clockTime < 0) clockTime = 0;
        currentClockTime = clockTime;
        uiManager.UpdateClockText(currentClockTime.ToString());
    }
}
