using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDetection : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public float criticalChance;
    public float criticalMultiplier;

    public GameObject hitFX;

    bool isCritical;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(hitFX, other.transform.position, Quaternion.identity);
            int damage = (int)CalculatedDamage();
            other.GetComponent<Health>().DeductHealth(damage);
            other.GetComponent<AttackedScrollingText>().OnAttack(damage, isCritical);
        }
    }

    float CalculatedDamage()
    {
        float damage;
        damage = Random.Range(minDamage, maxDamage);
        float rand = Random.value;
        if (rand <= criticalChance)
        {
            isCritical = true;
            damage *= criticalMultiplier;
        }
        else
        {
            isCritical = false;
        }
        return damage;
    }
}
