using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDetection : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public float criticalChance;
    public float criticalMultiplier;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int damage = (int)CalculatedDamage();
            other.GetComponent<Health>().DeductHealth(damage);
        }
    }
    float CalculatedDamage()
    {
        float damage;
        damage = Random.Range(minDamage, maxDamage);
        float rand = Random.value;
        if (rand <= criticalChance)
        {
            damage *= criticalMultiplier;
        }
        return damage;
    }
}
