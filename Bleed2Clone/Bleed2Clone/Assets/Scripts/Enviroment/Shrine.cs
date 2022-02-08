using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    bool playerInside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerPrefs.SetFloat("saveX", collision.transform.position.x);
            PlayerPrefs.SetFloat("saveY", collision.transform.position.y);
            collision.GetComponent<Damagable>().Heal();
        }
    }
}
