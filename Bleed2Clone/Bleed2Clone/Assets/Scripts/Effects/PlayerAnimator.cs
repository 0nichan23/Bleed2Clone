using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer[] SR;
    [SerializeField] private Transform gfxParent;
    [SerializeField] private Animator anim;
    private float flippedSign;
    private bool isGrounded;

    private void Start()
    {
        GameEvents.OnPlayerGroundedEvent += UpdateIsGrounded;
        flippedSign = 1f;
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
