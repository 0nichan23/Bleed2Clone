using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private Animator anim;
    private bool isGrounded;

    private void Start()
    {
        GameEvents.OnPlayerGroundedEvent += UpdateIsGrounded;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerGroundedEvent -= UpdateIsGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        float horiVelocity = rb2d.velocity.x;
        float vertiVelocity = rb2d.velocity.y;
        anim.SetFloat("HoriSpeed", Mathf.Abs(horiVelocity));
        anim.SetFloat("VertiSpeed", vertiVelocity);
        if (horiVelocity < -0.5f && !SR.flipX)
            SR.flipX = true;
        else if (horiVelocity > 0.5f && SR.flipX)
            SR.flipX = false;
    }

    private void UpdateIsGrounded(bool grounded)
    {
        anim.SetBool("IsGrounded", grounded);
        isGrounded = grounded;

        if (grounded) SquashAndStretch(new Vector2(1.2f, 0.8f), 10f, true);
        else SquashAndStretch(new Vector2(0.9f, 1.1f), 2f, true);
    }

    private void SquashAndStretch(Vector2 amount, float rate, bool shouldReturn)
    {
        StartCoroutine(DoSquashAndStretch(amount, rate, shouldReturn));
    }

    private IEnumerator DoSquashAndStretch(Vector2 amount, float rate, bool shouldReturn)
    {
        Transform srTrans = SR.transform;

        float lerpAmount = 0f;
        while(lerpAmount < 1f)
        {
            lerpAmount += rate * Time.deltaTime;
            Vector3 scale = srTrans.localScale;
            scale.x = Mathf.Lerp(1f, amount.x, lerpAmount);
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
                scale.x = Mathf.Lerp(amount.x, 1f, lerpAmount);
                scale.y = Mathf.Lerp(amount.y, 1f, lerpAmount);
                srTrans.localScale = scale;
                yield return null;
            }
        }
    }
}
