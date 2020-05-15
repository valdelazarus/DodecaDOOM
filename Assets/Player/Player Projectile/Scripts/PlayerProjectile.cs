using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public Vector3 direction;

    Rigidbody rb;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void UpdateAnimation(float lastMoveX, float lastMoveY)
    {
        if (lastMoveX == -1f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (lastMoveX == 1f)
        {
            direction = new Vector3(1f, 0f, 0f);
        }
        if (lastMoveX == -1f)
        {
            direction = new Vector3(-1f, 0f, 0f);
        }
        if (lastMoveY == 1f)
        {
            direction = new Vector3(0f, 0f, 1f);
        }
        if(lastMoveY == -1f)
        {
            direction = new Vector3(0f, 0f, -1f);
        }

        anim.SetFloat("lastMoveX", lastMoveX);
        anim.SetFloat("lastMoveY", lastMoveY);
    }
    void OnTriggerEnter(Collider other)
    {
        //deal damage to enemy
        Destroy(gameObject);
    }
}
