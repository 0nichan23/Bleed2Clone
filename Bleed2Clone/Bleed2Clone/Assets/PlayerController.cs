using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Dashspeed;
    public float JumpForce;
    float MoveInput;
    float MoveInputVer;
    bool FaceRight;
    private Rigidbody2D rb;
    public bool isGrounded;
    public Transform groundCheck;
    public float CheckRadius;
    public LayerMask whatIsGround;
    public int ExtraJumps;
    int jumpsLeft;
    public float Dashtime;
    public float DashDuration;
    Vector2 direction;
    public bool isdashing;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);
        MoveInput = Input.GetAxis("Horizontal");
        MoveInputVer = Input.GetAxis("Vertical");
        direction = new Vector2(MoveInput, MoveInputVer);
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                jumpsLeft = ExtraJumps;
                isdashing = false;
                DashDuration = 0;
                Jump();
            }
        }
        if (jumpsLeft > 0 && isGrounded == false)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                DashDuration = 0;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (isdashing == false)
                {
                    jumpsLeft--;
                    StartCoroutine(Dashing());
                }
            }
        }

    }
    void Movement()
    {
        if (FaceRight == false && MoveInput < 0)
        {
            Flip();
        }
        else if (FaceRight == true && MoveInput > 0)
        {
            Flip();
        }
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = Vector2.up * JumpForce;   
    }

    void Dash()
    {
        
    }
    void Flip()
    {
        FaceRight = !FaceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    IEnumerator Dashing(float timetowait = 0.3f)
    {
        DashDuration = Dashtime;
        while (DashDuration > 0)
        {
            Debug.Log("suppose to dash");
             isdashing = true;
             yield return new WaitForSeconds(timetowait);
             rb.velocity = direction * Dashspeed;
             DashDuration -= timetowait;
             rb.gravityScale = 0.0f;
        }
        isdashing = false;
        rb.gravityScale = 3.0f;
    }
}
