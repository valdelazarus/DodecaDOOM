using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public enum EffectType
    {
        ATTACK_1, ATTACK_2, HURT, REVIVED, PICKUP
    };

    public AudioClip attack1Clip, attack2Clip, hurtClip, revivedClip, pickUpClip;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void PlaySoundEffect(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.ATTACK_1:
                audioSource.PlayOneShot(attack1Clip);
                break;
            case EffectType.ATTACK_2:
                audioSource.PlayOneShot(attack2Clip);
                break;
            case EffectType.HURT:
                audioSource.PlayOneShot(hurtClip);
                break;
            case EffectType.REVIVED:
                audioSource.PlayOneShot(revivedClip);
                break;
            case EffectType.PICKUP:
                audioSource.PlayOneShot(pickUpClip);
                break;
        }
    }
}
