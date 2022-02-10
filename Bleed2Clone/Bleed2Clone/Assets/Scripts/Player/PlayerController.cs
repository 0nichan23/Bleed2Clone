using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
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
    private bool _isGrounded = false;
    public bool isGrounded
    {
        get => _isGrounded;
        set
        {
            if (_isGrounded != true && value == true)
            {
                AudioManager.instance.PlaySFX(audioSource, SFX_Type.land);
            }
            _isGrounded = value;
        }
    }
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
    private int dashesLeft;
    public float Dashspeed;
    public float DashForce;
    public float DashCd;
    public float DashDuration;

    float lastDash;
    bool isDashing;

    [Header("Controls")]
    public FixedJoystick floatingJoystick;
    public bool usePCControls;

    internal AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        jumpsLeft = ExtraJumps;

        // first save
        PlayerPrefs.SetFloat("saveX", transform.position.x);
        PlayerPrefs.SetFloat("saveY", transform.position.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, whatIsGround);
        if (!usePCControls)
        {
            MoveInput = floatingJoystick.Horizontal;
            MoveInputVer = floatingJoystick.Vertical;
        }
        else if (usePCControls)
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            MoveInputVer = Input.GetAxisRaw("Vertical");
        }
        if (!isDashing)
        {
            Movement();

            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpsLeft > 0)
            {
                Jump();
            }
        }
        if (isGrounded)
        {
            jumpsLeft = ExtraJumps;
            dashesLeft = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CheckIfCanDash();
        }
    }

    void Movement()
    {
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
    }
    void CheckIfCanDash()
    {
        if (Time.time - lastDash <= DashCd || isGrounded || dashesLeft < 1)
        {
            return;
        }
        lastDash = Time.time;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        AudioManager.instance.PlaySFX(audioSource, SFX_Type.dash);

        dashesLeft--;
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
        AudioManager.instance.PlaySFX(audioSource, SFX_Type.jump);
        rb.velocity = Vector2.up * JumpForce;
        jumpsLeft--;
    }
}
