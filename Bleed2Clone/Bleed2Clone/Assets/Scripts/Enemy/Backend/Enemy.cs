using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    Created,
    PlayerInRange,
    PlayerNotInRange,
    Death
}
public abstract class Enemy : ScriptableObject
{
    public readonly EnemyState state;
    public GameObject enemyPrefab;
    public GameObject enemyRagDoll;
    public abstract void OnCreated();
    public abstract void PlayerInRangeBehaviour();
    public abstract void PlayerNotInRangeBehaviour();
    public abstract void Die();
}
