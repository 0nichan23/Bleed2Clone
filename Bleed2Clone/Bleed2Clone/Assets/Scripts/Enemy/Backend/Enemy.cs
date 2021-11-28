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
public abstract class Enemy
{
    public readonly EnemyState state = EnemyState.PlayerNotInRange;
    public abstract void OnCreated();
    public abstract void PlayerInRangeBehaviour();
    public abstract void PlayerNotInRangeBehaviour();
    public abstract void Die();
}
