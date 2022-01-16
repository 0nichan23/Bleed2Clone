using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticle : MonoBehaviour
{
    [SerializeField] private GameObject deathParticlesPrefab;
    private GameObject myParticles;

    private void Start()
    {
        myParticles = Instantiate(deathParticlesPrefab);
        myParticles.SetActive(false);
    }

    private void OnEnable()
    {
        myParticles.SetActive(false);
    }

    private void OnDisable()
    {
        myParticles.transform.position = transform.position;
        myParticles.SetActive(true);
    }
}
