using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Transform weapon;

    internal Transform player;
    internal NavMeshAgent2D agent;
    internal Rigidbody2D rb;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
    private EnemyState _state = EnemyState.Created;
    private float hp;
    internal Vector2 lastGroundedPos;
    public EnemyState State => _state;
    internal bool isGrounded => IsGrounded();
    internal EnemyDatabase database;

    private void Start()
    {
        if (FindObjectOfType<PlayerController>())
            player = FindObjectOfType<PlayerController>().gameObject.transform;

        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent2D>();
        agent.enabled = false;
        hp = database.MaxHP;

    }
    private void Update()
    {
        if (agent.enabled && player != null)
            if (DistanceFromPlayer() <= database.enemyRange)
            {
                SwitchState(EnemyState.PlayerInRange);
            }
            else
            {
                SwitchState(EnemyState.PlayerNotInRange);
            }
    }
    private float DistanceFromPlayer()
    {
        return Vector2.Distance(this.transform.position, player.position);
    }
    public bool SwitchState(EnemyState state)
    {
        if (state != _state)
            _state = state;
        else return false;

        switch (_state)
        {
            case EnemyState.PlayerNotInRange:
                database.PlayerNotInRangeBehaviour(this);
                break;
            case EnemyState.PlayerInRange:
                database.PlayerInRangeBehaviour(this);
                break;
            case EnemyState.Death:
                database.OnDeath(this);
                break;
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded)
        {
            agent.enabled = true;
        }
    }

    private bool IsGrounded()
    {
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, direction, distance, ground);
        if (hit.collider != null)
        {
            lastGroundedPos = transform.position;
            return true;
        }

        return false;
    }
}
