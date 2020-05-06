using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody myRB;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    
    void Update()
    {
        ProcessMovement();

    }

    private void ProcessMovement()
    {
        float vert = CrossPlatformInputManager.GetAxis("Vertical");
        float horz = CrossPlatformInputManager.GetAxis("Horizontal");

        myRB.velocity = new Vector3(
                horz,
                myRB.velocity.y,
                vert
            ) * speed;
    }
}
