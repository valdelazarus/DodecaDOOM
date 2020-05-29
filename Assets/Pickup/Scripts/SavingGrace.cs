using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingGrace : MonoBehaviour
{
    public float lifeTime;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerSound>().PlaySoundEffect(PlayerSound.EffectType.PICKUP);
            FindObjectOfType<GameManager>().AddSavingGrace();
            Destroy(gameObject);
        }
    }
}
