
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
        float score = PlayerPrefs.GetFloat("Score");
        scoreText.text = "Final Score: " + score;
        PlayerPrefs.SetFloat("Score", 0);
    }
    
    void Update()
    {
        
    }
}
