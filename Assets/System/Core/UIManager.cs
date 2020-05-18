using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text clockText;
    public Image healthBar;
    public Text scoreText;

    public void UpdateClockText(string timeText)
    {
        clockText.text = "Current Clock Time: " + timeText;
    }

    public void UpdateHealthBar(float current, float max)
    {
        float barValue = current / max;
        healthBar.GetComponent<RectTransform>().localScale = new Vector3(barValue, 1f, 1f);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
