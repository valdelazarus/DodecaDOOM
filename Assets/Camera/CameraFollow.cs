using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camSpeed;
    Transform player;

    Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        offset = transform.position - player.position;
    }

    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offset + player.position, camSpeed * Time.deltaTime);
    }
}
