using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public enum EffectType
    {
        APPEAR, DISAPPEAR
    };

    public AudioClip appearClip, disappearClip;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.APPEAR:
                audioSource.PlayOneShot(appearClip);
                break;
            case EffectType.DISAPPEAR:
                audioSource.PlayOneShot(disappearClip);
                break;
        }
    }
}
