using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    Created,
    Idle,
    Patrol,
    FollowPlayer,
    ShootPlayer,
    Death
}
public abstract class Enemy : ScriptableObject
{
    [SerializeField] EnemyAI enemyAI;

    public readonly EnemyState state = EnemyState.Idle;
    public abstract void OnCreated();
    public abstract void IdleBehaviour();
    public abstract void PatrolBehaviour();
    public abstract void FollowPlayerBehavior();
    public abstract void ShootBehavior();
    public abstract void Die();
}
