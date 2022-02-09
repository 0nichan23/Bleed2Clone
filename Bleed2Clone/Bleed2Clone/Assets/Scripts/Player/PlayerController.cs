using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    bool FaceRight;

    [Header("Movement")]
    public float speed;
    public float JumpForce;

    [Header("Inputs")]
    public float MoveInput;
    public float MoveInputVer;

    [Header("Collisions")]
    public bool isGrounded;
    public Transform groundCheck;
    public float CheckRadius;
    public LayerMask whatIsGround;

    [Header("Variables")]
    public Camera cam;
    public Transform holder;
    
    [Header("Jump")]
    public int ExtraJumps;
    public int jumpsLeft;
    public float jumpCd;

    float lastJump;

    [Header("Dash")]
    public float Dashspeed;
    public float DashForce;
    public float DashCd;
    public float DashDuration;

    float lastDash;
    bool isDashing;

    [Header("Controls")]
    public FixedJoystick floatingJoystick;
    public bool usePCControls;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = ExtraJumps;

        // first save
        PlayerPrefs.SetFloat("saveX", transform.position.x);
        PlayerPrefs.SetFloat("saveY", transform.position.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);
        MoveInput = floatingJoystick.Horizontal;
        MoveInputVer = floatingJoystick.Vertical;
        if (usePCControls)
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            MoveInputVer = Input.GetAxisRaw("Vertical");
        }
        if (!isDashing)
        {
            Movement();
            if (MoveInputVer > 0.6 && jumpsLeft > 0)
            {
                Jump();
            }
        }
        if (isGrounded)
        {
            jumpsLeft = ExtraJumps;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CheckIfCanDash();
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
    void CheckIfCanDash()
    {
        if (Time.time - lastDash <= DashCd)
        {
            return;
        }
        lastDash = Time.time;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = new Vector2(MoveInput, MoveInputVer);
        Vector2 direction = new Vector2(MoveInput, MoveInputVer);
        rb.AddForce(direction * DashForce, ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(DashDuration);
        isDashing = false;
        rb.gravityScale = gravity;
    }

    void Jump()
    {
        if (Time.time - lastJump <= jumpCd)
        {
            return;
        }
        lastJump = Time.time;
        rb.velocity = Vector2.up * JumpForce;
        jumpsLeft--;
        Debug.Log(jumpsLeft);
    }

    void Flip()
    {
        /*FaceRight = !FaceRight;
        transform.Rotate(0f, 180f, 0f); */
    }


}
