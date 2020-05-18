using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public bool isPlayer;

    float health;

    UIManager uiManager;
    Animator anim; 

    void Start()
    {
        health = maxHealth;
        if (isPlayer)
        {
            uiManager = FindObjectOfType<UIManager>();
            uiManager.UpdateHealthBar(health, maxHealth);
        }
        
        anim = GetComponent<Animator>();
    }


    public void AddHealth(float amount)
    {
        health += amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        if (isPlayer)
        {
            uiManager.UpdateHealthBar(health, maxHealth);
        }
        
    }

    public void DeductHealth(float amount)
    {
        health -= amount;
        if (isPlayer)
        {
            uiManager.UpdateHealthBar(health, maxHealth);
        }
        if (health <= 0)
        {
            GetComponent<Enemy>()?.AddScore();
            if (anim)
            {
                anim.SetTrigger("die");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
