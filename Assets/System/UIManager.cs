using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text clockText;

    public void UpdateClockText(string timeText)
    {
        clockText.text = "Current Clock Time: " + timeText;
    }
}
