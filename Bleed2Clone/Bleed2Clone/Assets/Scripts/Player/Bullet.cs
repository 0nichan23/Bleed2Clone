using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Bullet : MonoBehaviour
{
    [SerializeField] float LifeTime = 1.5f;
    [SerializeField] float speed;
    [SerializeField] List<int> layersIHit;

    Rigidbody2D rb;
    internal Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 tmp = direction.normalized;
        rb.velocity = tmp * speed;
    }

    private void OnEnable()
    {
        Invoke("Disable", LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layersIHit.Contains(collision.gameObject.layer))
        {
            Disable();
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
