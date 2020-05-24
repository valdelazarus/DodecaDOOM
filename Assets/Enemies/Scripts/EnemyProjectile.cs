using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public Vector3 direction;

    public float minDamage;
    public float maxDamage;
    public float criticalChance;
    public float criticalMultiplier;

    Rigidbody rb;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void UpdateAnimation(float lastMoveX, float lastMoveY)
    {

        if (lastMoveX >= 1f)
        {
            direction = new Vector3(1f, 0f, 0f);
        }
        if (lastMoveX <= -1f)
        {
            direction = new Vector3(-1f, 0f, 0f);
        }
        if (lastMoveY >= 1f)
        {
            direction = new Vector3(0f, 0f, 1f);
        }
        if (lastMoveY <= -1f)
        {
            direction = new Vector3(0f, 0f, -1f);
        }

        anim.SetFloat("lastMoveX", lastMoveX);
        anim.SetFloat("lastMoveY", lastMoveY);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int damage = (int)CalculatedDamage();
            other.GetComponent<Health>().DeductHealth(damage);
        }

        Destroy(gameObject);
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
