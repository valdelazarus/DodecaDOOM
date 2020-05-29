
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinalScoreDisplay : MonoBehaviour
{
    Text scoreText;
    void Start()
    {
        scoreText = GetComponent<Text>();
        int score = PlayerPrefs.GetInt("Score");
        scoreText.text = "Final Score: " + score;
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("SavingGrace", 0);
        PlayerPrefs.SetInt("ClockTime", 0);
    }
    
    void Update()
    {
        
    }
}
