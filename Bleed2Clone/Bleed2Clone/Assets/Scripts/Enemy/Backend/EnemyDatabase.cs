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

public abstract class EnemyDatabase : ScriptableObject
{
    public float MaxHP;
    public float enemyRange;

    public GameObject enemyPrefab;
    public GameObject enemyRagDoll;

    public List<Enemy> activeEnemiesFromThisType;

    public abstract void OnCreated(Enemy enemy);
    public abstract void PlayerInRangeBehaviour(Enemy enemy);
    public abstract void PlayerNotInRangeBehaviour(Enemy enemy);
    public abstract void OnDeath(Enemy enemy);
    public void SpawenEnemy(Transform spawnPoint)
    {
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.database = this;
        activeEnemiesFromThisType.Add(enemy);
    }
}
