using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Transform weapon;

    internal Transform player;
    internal NavMeshAgent2D agent;
    private EnemyState _state = EnemyState.Created;
    private float hp;

    public EnemyState State => _state;

    internal EnemyDatabase database;

    private void Start()
    {
        if (FindObjectOfType<PlayerController>())
            player = FindObjectOfType<PlayerController>().gameObject.transform;

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
        if (collision.gameObject.layer == 7)
        {
            agent.enabled = true;
        }
    }
}
