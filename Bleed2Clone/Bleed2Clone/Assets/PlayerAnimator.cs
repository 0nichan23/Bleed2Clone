using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private Animator anim;

    // Update is called once per frame
    void Update()
    {
        float horiVelocity = rb2d.velocity.x;
        anim.SetFloat("HoriSpeed", Mathf.Abs(horiVelocity));
        if (horiVelocity < -0.5f)
            SR.flipX = true;
        else if (horiVelocity > 0.5f)
            SR.flipX = false;
    }
}
