using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    private Transform player;
    private EnemyState _state;
    private float hp;

    public EnemyState State => _state;

    internal EnemyDatabase database;

    private void Start()
    {
        if (FindObjectOfType<PlayerController>())
            player = FindObjectOfType<PlayerController>().gameObject.transform;

        hp = database.MaxHP;
    }
    private void Update()
    {
        if (player != null && DistanceFromPlayer() <= database.enemyRange)
        {
            _state = EnemyState.PlayerInRange;
        }
        else
        {
            _state = EnemyState.PlayerNotInRange;
        }
    }
    private float DistanceFromPlayer()
    {
        return Vector2.Distance(this.transform.position, player.position);
    }
    public void SwitchState(Enemy enemy, EnemyState state)
    {
        _state = state;

        switch (_state)
        {
            case EnemyState.Created:
                database.OnCreated(this);
                break;
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
    }
}
