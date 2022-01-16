using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip gameLoop;

    private void Start()
    {
        StartCoroutine(PlayGameMusic());
    }

    IEnumerator PlayGameMusic()
    {
        audioSource.clip = intro;
        audioSource.Play();
        yield return new WaitForSeconds(intro.length-1);
        audioSource.clip = gameLoop;
        audioSource.loop = true;
        audioSource.Play();
    }
}
