using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer[] SR;
    [SerializeField] private Transform gfxParent;
    [SerializeField] private Sprite[] headDirections;
    [SerializeField] private Animator anim;
    [SerializeField] private FixedJoystick shootJoystick;
    [SerializeField] private PlayerController playerController;
    private PlayerWeapon shoot;
    private float flippedSign;
    private bool isGrounded;
    private bool isShootInput => playerController.usePCControls? Input.GetMouseButton(0) : Input.touchCount > 0;


    private void Start()
    {
        GameEvents.OnPlayerGroundedEvent += UpdateIsGrounded;
        flippedSign = 1f;
        shoot = FindObjectOfType<PlayerWeapon>();
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerGroundedEvent -= UpdateIsGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        bool changedFlip = false;
        float horiVelocity = rb2d.velocity.x;
        float vertiVelocity = rb2d.velocity.y;
        anim.SetFloat("HoriSpeed", Mathf.Abs(horiVelocity));
        anim.SetFloat("VertiSpeed", vertiVelocity);
        if (horiVelocity < -0.5f && flippedSign == 1)
        {
            flippedSign = -1;
            changedFlip = true;
        }
        else if (horiVelocity > 0.5f && flippedSign == -1)
        {
            flippedSign = 1;
            changedFlip = true;
        }

        if (changedFlip)
        {
            gfxParent.localScale = new Vector3(flippedSign, 1, 1);
        }

        if(isShootInput) //if there's input in the shoot joystick
        {
            anim.SetBool("IsShooting", true); //Set the shooting bool for the standing-throw animation;           
            //SR[0] should be the head-GFX
            //Match the flip to where the the look direction

            if(gfxParent.localScale.x > 0) //If the body is facing to the right (1 in X scale)
                SR[0].flipX = (shoot.shootVector.x < 0) ? true : false;  //Return according to stick direction
            else //If the body is facing to the left (-1 in X scale)
                SR[0].flipX = (shoot.shootVector.x > 0) ? true : false; //Do the same but in reverse to match the -1

            //Match the sprite to the direction

            // will return a value between 0 and 180 depending on the aim. (EX. up = 0, down = 180, left/right = 90)
            float angle = Vector2.Angle(Vector2.up, shoot.shootVector);
            //There are 5 head directions and 180 degrees, we give each head-direction an equal range
            float rangePerHeadDir = 180 / 5;

            //Round up (ceil = round-up) the current angle divided by the range per head to get the index of which head we need to pick now;
            int currentFace = Mathf.CeilToInt(angle / rangePerHeadDir);

            //Set the correct head
            SR[0].sprite = headDirections[currentFace - 1];
        }
        else //If there isn't any input currently
        {
            SR[0].sprite = headDirections[2];
            SR[0].flipX = false;
            anim.SetBool("IsShooting", false);
        }
    }

    private void UpdateIsGrounded(bool grounded)
    {
        anim.SetBool("IsGrounded", grounded);
        isGrounded = grounded;

        if (grounded) SquashAndStretch(new Vector2(1.3f, 0.7f), 10f, true);
        else SquashAndStretch(new Vector2(0.8f, 1.2f), 2f, true);
    }

    private void SquashAndStretch(Vector2 amount, float rate, bool shouldReturn)
    {
        StartCoroutine(DoSquashAndStretch(amount, rate, shouldReturn));
    }

    private IEnumerator DoSquashAndStretch(Vector2 amount, float rate, bool shouldReturn)
    {
        Transform srTrans = gfxParent;
        float currentFlippedSign = flippedSign;

        float lerpAmount = 0f;
        while(lerpAmount < 1f)
        {
            lerpAmount += rate * Time.deltaTime;
            Vector3 scale = srTrans.localScale;
            scale.x = Mathf.Lerp(flippedSign, amount.x * flippedSign, lerpAmount);
            scale.y = Mathf.Lerp(1f, amount.y, lerpAmount);
            srTrans.localScale = scale;
            yield return null;
        }

        if (shouldReturn)
        {
            lerpAmount = 0f;
            while (lerpAmount < 1)
            {
                lerpAmount += rate * Time.deltaTime;
                Vector3 scale = srTrans.localScale;
                scale.x = Mathf.Lerp(amount.x * flippedSign, flippedSign, lerpAmount);
                scale.y = Mathf.Lerp(amount.y, 1f, lerpAmount);
                srTrans.localScale = scale;
                yield return null;
            }
        }
    }
}
