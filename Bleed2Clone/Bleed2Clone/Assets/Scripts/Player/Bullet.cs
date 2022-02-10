using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Bullet : MonoBehaviour
{
    [SerializeField] float LifeTime = 1.5f;
    [SerializeField] float speed;
    [SerializeField] List<int> layersIHit;
    ApplyDamage applyDamage;
    private float lifetimeTimer;

    Rigidbody2D rb;
    internal Vector2 direction;

    Renderer _renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        applyDamage = GetComponent<ApplyDamage>();
    }

    private void FixedUpdate()
    {
        Vector2 tmp = direction.normalized;
        rb.velocity = tmp * speed;
    }

    private void Update()
    {
        lifetimeTimer -= Time.deltaTime;

        if(lifetimeTimer <= 0)
        {
            Disable();
        }

        if (!IsVisibleToCamera(transform))
        {
            Disable();
        }
    }

    private void OnEnable()
    {
        // Invoke("Disable", LifeTime); This will cause bullets that were disabled and enabled to dissapear prematurely
        //Switching to timer that resets on OnEnable()
        lifetimeTimer = LifeTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(name + " Collides with" + collision.gameObject.name);
        if (layersIHit.Contains(collision.gameObject.layer))
        {
            if (collision.gameObject.GetComponent<Damagable>())
                applyDamage.ApplyDamageToDamagable(collision.gameObject.GetComponent<Damagable>());

            Disable();
        }
    }
    public static bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
