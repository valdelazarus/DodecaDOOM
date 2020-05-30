using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public GameObject attachedHealthBar;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (currentHealth < 0) currentHealth = 0;
        attachedHealthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }
}
