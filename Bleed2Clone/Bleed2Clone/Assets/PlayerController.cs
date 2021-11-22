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
    float DashtimeCounter;
    Vector2 direction;
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
                Jump();
            }
            else if (jumpsLeft > 0)
            {
                DashtimeCounter = Dashtime;
                jumpsLeft--;
                Dash();
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
        Debug.Log("dashing");
        if (Input.GetKey(KeyCode.Space))
        {

        }
        rb.velocity = direction * speed;
        //DashtimeCounter -= Time.deltaTime;
    }
    void Flip()
    {
        FaceRight = !FaceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
