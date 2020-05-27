using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoaderTimer : MonoBehaviour
{
    public float waitDuration;

    Text timerText;
    LevelManager levelManager;

    float currentWaitTime;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        timerText = GetComponent<Text>();

        currentWaitTime = waitDuration;
        timerText.text = currentWaitTime.ToString();
    }  
    void Update()
    {
        StartCountdown();
    }

    void StartCountdown()
    {
        currentWaitTime -= Time.deltaTime;
        if (currentWaitTime < 0)
        {
            levelManager.LoadNextScene();
        }
        else
        {
            timerText.text = ((int)currentWaitTime).ToString();
        }
    }
}
