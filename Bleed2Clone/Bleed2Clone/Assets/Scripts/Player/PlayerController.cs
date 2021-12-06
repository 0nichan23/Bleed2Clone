using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Dashspeed;
    public float JumpForce;
    public float MoveInput;
    public float MoveInputVer;
    bool FaceRight;
    private Rigidbody2D rb;
    public bool isGrounded;
    public Transform groundCheck;
    public float CheckRadius;
    public LayerMask whatIsGround;
    public Camera cam;
    Vector3 mousePos;
    public Transform holder;
    public Transform gunz;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);
        MoveInput = Input.GetAxisRaw("Horizontal");
        MoveInputVer = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - holder.position;
        mousePos.Normalize();
        float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        holder.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        Movement();
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetMouseButton(0))
        {
            Shoot();
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

    void Flip()
    {
        /*FaceRight = !FaceRight;
        transform.Rotate(0f, 180f, 0f); */
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(gunz.position, gunz.right);
        if (hit)
        {
            Debug.Log(hit.transform.name);
        }
    }
}
