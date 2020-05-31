using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public bool isPlayer;
    public bool isDead;

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
        if (isDead) return;

        health -= amount;
        if (isPlayer)
        {
            uiManager.UpdateHealthBar(health, maxHealth);
            GetComponent<PlayerSound>().PlaySoundEffect(PlayerSound.EffectType.HURT);
        }
        if (GetComponent<BossHealthBar>())
        {
            GetComponent<BossHealthBar>().UpdateHealthBar(health, maxHealth);
        }

        if (health <= 0 && !isDead)
        {
            if (!isPlayer)
            {
                GetComponent<EnemySound>().PlaySoundEffect(EnemySound.EffectType.DISAPPEAR);
                GetComponent<Enemy>()?.AddScore();
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                GetComponent<PlayerController>().enabled = false;
            }
            if (anim)
            {
                anim.SetTrigger("dead");
            }
            else
            {
                Destroy(gameObject);
            }  
        }
        else
        {
            if (anim)
            {
                anim.SetTrigger("hit");
            }
        }
    }
    public void ResetHealth()
    {
        health = maxHealth;
        isDead = false;
        if (isPlayer)
        {
            uiManager.UpdateHealthBar(health, maxHealth);
        }
    }
}
