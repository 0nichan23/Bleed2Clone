using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemySpawning
{
    public EnemyDatabase enemyDatabase;
    public int amount;
}

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject poolPrefab;
    [SerializeField] List<EnemySpawning> enemySpawnings;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        foreach (EnemySpawning enemySpawning in enemySpawnings)
        {
            if (!enemySpawning.enemyDatabase.bulletPool)
            {
                GameObject pool = Instantiate(poolPrefab);
                pool.name = (enemySpawning.enemyDatabase.name + " Bullet Pool");
                enemySpawning.enemyDatabase.bulletPool = pool.GetComponent<ObjectPool>();
                enemySpawning.enemyDatabase.bulletPool.Init(enemySpawning.enemyDatabase.weaponPrefab.GetComponent<EnemyWeapon>().bulletPrefab);
            }

            for (int i = 0; i < enemySpawning.amount; i++)
            {
                enemySpawning.enemyDatabase.SpawnEnemy(transform);
            }
        }
    }

    void OnApplicationQuit()
    {
        foreach (EnemySpawning enemySpawning in enemySpawnings)
        {
            if (enemySpawning.enemyDatabase.activeEnemiesFromThisType.Count > 0)
            {
                enemySpawning.enemyDatabase.activeEnemiesFromThisType.Clear();
            }
        }
    }
}
