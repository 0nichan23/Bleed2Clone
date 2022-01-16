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
    public float stepsInSeconds = 3;
    public float cooldown = 1;

    public GameObject enemyPrefab;
    public GameObject enemyRagDoll;

    public List<Enemy> activeEnemiesFromThisType;

    public GameObject weaponPrefab;

    internal ObjectPool bulletPool;
    public abstract IEnumerator OnCreated(Enemy enemy);
    public abstract IEnumerator PlayerInRangeBehaviour(Enemy enemy);
    public abstract IEnumerator PlayerNotInRangeBehaviour(Enemy enemy);

    public void OnDeath(Enemy enemy)
    {
        activeEnemiesFromThisType.Remove(enemy);
        GameObject corpse = Instantiate(enemyRagDoll, enemy.gameObject.transform.position, Quaternion.identity);
        enemy.gameObject.SetActive(false);
    }
    public void SpawnEnemy(Transform spawnPoint)
    {
        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.database = this;
        activeEnemiesFromThisType.Add(enemy);
        enemy.database.OnCreated(enemy);
    }
}
