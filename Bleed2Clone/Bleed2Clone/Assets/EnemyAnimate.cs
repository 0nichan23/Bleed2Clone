using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer SR;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == false) anim = GetComponent<Animator>(); //failsafe

        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Basic modular animation implementation, good enough for now
        
        if(lastPos != transform.position)
        {
            anim.SetBool("Moving", true);
            SR.flipX = (Mathf.Sign(transform.position.x - lastPos.x) > 0)? true:false;
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        lastPos = transform.position;


    }
}
