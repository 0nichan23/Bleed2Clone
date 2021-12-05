using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    private Transform player;
    private EnemyState _state;
    private float hp;

    public EnemyState state => _state;

    internal EnemyDatabase database;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        hp = database.MaxHP;
    }
    private void Update()
    {
        if (DistanceFromPlayer() <= database.enemyRange)
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
}
