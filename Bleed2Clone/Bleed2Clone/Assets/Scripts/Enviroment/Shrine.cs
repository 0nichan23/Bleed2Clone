using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Shrine : MonoBehaviour
{
    bool playerInside;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioManager.instance.PlaySFX(audioSource, SFX_Type.shrine);

            PlayerPrefs.SetFloat("saveX", collision.transform.position.x);
            PlayerPrefs.SetFloat("saveY", collision.transform.position.y);
            collision.GetComponent<Damagable>().Heal();
        }
    }
}
