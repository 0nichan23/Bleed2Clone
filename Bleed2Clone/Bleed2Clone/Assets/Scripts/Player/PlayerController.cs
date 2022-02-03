using System.Collections;
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
    Vector3 directionPointed;
    public Transform holder;
    public int ExtraJumps;
    int jumpsLeft;
    public float jumpCd;
    float lastJump;
    public float DashForce;
    public float DashCd;
    float lastDash;
    bool isDashing;
    public float DashDuration;

    public FixedJoystick floatingJoystick;

    [SerializeField] private bool usePCControls;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = ExtraJumps;
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
        /*      directionPointed = cam.ScreenToWorldPoint(Input.mousePosition) - holder.position;
                directionPointed.Normalize();
                float rotationZ = Mathf.Atan2(directionPointed.y, directionPointed.x) * Mathf.Rad2Deg;
                holder.rotation = Quaternion.Euler(0f, 0f, rotationZ);*/
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
