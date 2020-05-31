using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text clockText;
    public Image healthBar;
    public Text scoreText;
    public Image ability1Image, ability2Image;
    public GameObject pauseMenu;
    public Text savingGraceCountText;
    public GameObject revivingIndicator;
    public GameObject rulesPanel;
    public GameObject bossInfoPanel;
    public Text bossInfoText;
    public float bossInfoPanelDisplayDuration;
    public GameObject challengeCountdownPanel;
    public Text challengeCountdownText;

    public void UpdateClockText(string timeText)
    {
        if (timeText == "0") timeText = "12 PM";
        else if (timeText == "12") timeText = "12 AM";
        else timeText += " PM";

        clockText.text = timeText;
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

    public void ShowAbilityInCooldown(int abilityNumber)
    {
        if (abilityNumber == 1)
        {
            ability1Image.color = Color.gray;
        }
        else if (abilityNumber == 2)
        {
            ability2Image.color = Color.gray;
        }
       
    }
    public void ShowAbilityIsAvailable(int abilityNumber)
    {
        if (abilityNumber == 1)
        {
            ability1Image.color = Color.white; 
        }
        else if (abilityNumber == 2)
        {
            ability2Image.color = Color.white;
        }
    }
    public void SetActivePauseMenu(bool active)
    {
        pauseMenu.SetActive(active);
    }
    public void UpdateSavingGraceCountText(int savingGraceCount)
    {
        savingGraceCountText.text = "x " + savingGraceCount;
    }
    public void ShowRevivingIndicator()
    {
        revivingIndicator.SetActive(true);
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
    }
    public void HideRules()
    {
        rulesPanel.SetActive(false);
    }
    public void DisplayBossInfo(string bossForm)
    {
        bossInfoPanel.SetActive(true);
        bossInfoText.text = "Boss - " + bossForm + " Form appeared!";
        Invoke("HideBossInfoPanel", bossInfoPanelDisplayDuration);
        if (bossForm == "Final")
        {
            Invoke("ShowChallengeCountdownPanel", bossInfoPanelDisplayDuration);
        }
    }

    void HideBossInfoPanel()
    {
        bossInfoPanel.SetActive(false);
        bossInfoText.text = "Boss - ? Form appeared!";
    }
    void ShowChallengeCountdownPanel()
    {
        challengeCountdownPanel.SetActive(true);
    }
    public void UpdateChallengeCountdownText(float realTimeLeft)
    {
        int min = (int)(realTimeLeft / 60f);
        int sec = (int)(realTimeLeft % 60f);

        string minText, secText;
        if (min < 10)
        {
            minText = "0" + min;
        }
        else
        {
            minText = min.ToString();
        }
        if (sec < 10)
        {
            secText = "0" + sec;
        }
        else
        {
            secText = sec.ToString();
        }

        challengeCountdownText.text = minText + ":" + secText;
    }
}
