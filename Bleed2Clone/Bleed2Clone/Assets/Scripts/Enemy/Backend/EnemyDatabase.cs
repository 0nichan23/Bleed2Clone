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

    public WeaponDatabase weapon;
    public abstract void OnCreated(Enemy enemy);
    public abstract void PlayerInRangeBehaviour(Enemy enemy);
    public abstract void PlayerNotInRangeBehaviour(Enemy enemy);

    public void OnDeath(Enemy enemy)
    {
        activeEnemiesFromThisType.Remove(enemy);
        GameObject corpse = Instantiate(enemyRagDoll, enemy.gameObject.transform.position, Quaternion.identity);
        Destroy(enemy.gameObject);
    }
    public void SpawnEnemy(Transform spawnPoint)
    {
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.database = this;
        activeEnemiesFromThisType.Add(enemy);
        enemy.database.OnCreated(enemy);
    }
}
