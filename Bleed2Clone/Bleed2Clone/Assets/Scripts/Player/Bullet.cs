using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    Vector2 direction;

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
        direction = Shoot.shootVector;
        Invoke("Disable", 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 7)
        {
            Disable();
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);

    }



}
