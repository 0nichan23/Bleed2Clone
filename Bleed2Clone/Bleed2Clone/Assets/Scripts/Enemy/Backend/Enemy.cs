using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    private Transform player;
    private EnemyState _state = EnemyState.Created;
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
        if (player != null)
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
}
