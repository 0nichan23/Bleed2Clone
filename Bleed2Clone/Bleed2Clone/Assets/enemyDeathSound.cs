using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class enemyDeathSound : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX(audioSource, SFX_Type.enemyDeath);
    }
}
