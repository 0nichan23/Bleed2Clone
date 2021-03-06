using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
    [SerializeField] private bool _ignoreGround = false;

    private Vector3 _target;

    public bool Move { internal get; set; }
    public bool IsGrounded { internal get => CheckGrounded(); set { } }

    internal Vector2 lastGroundedPos;

    void Update()
    {
        if (Move && _ignoreGround)
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (Move && IsGrounded)
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public void SetDestination(Vector3 target)
    {
        _target = target;
    }
    public void SetMovement(bool ableToMove)
    {
        Move = ableToMove;
    }
    public void SetIgnoreGround(bool ignoreGround)
    {
        _ignoreGround = ignoreGround;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    private bool CheckGrounded()
    {

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.25f, ground);

        if (isGrounded)
        {
            lastGroundedPos = new Vector3(transform.position.x, transform.position.y, 0);
            return true;
        }

        return false;
    }
}
