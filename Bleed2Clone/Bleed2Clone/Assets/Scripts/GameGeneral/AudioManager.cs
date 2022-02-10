using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX_Type
{
    jump,
    land,
    dash,
    shurikanShoot,
    shurikanHit,
    step,
    shrine,
    slowTime,
    regTime,
    playerHit,
    playerDeath,
    enemyHit,
    enemyDeath
}

public class SFX
{
    public AudioClip audio;
    public SFX_Type type;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip gameLoop;
    [SerializeField] List<SFX> sfxPool;

    private static AudioManager _instance;
    public static AudioManager instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        StartCoroutine(PlayGameMusic());
    }

    IEnumerator PlayGameMusic()
    {
        audioSource.clip = intro;
        audioSource.Play();
        yield return new WaitForSeconds(intro.length - 1);
        audioSource.clip = gameLoop;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySFX(AudioSource audioSource, SFX_Type type)
    {
        audioSource.clip = sfxPool.Find(x => x.type == type).audio;
    }

}
