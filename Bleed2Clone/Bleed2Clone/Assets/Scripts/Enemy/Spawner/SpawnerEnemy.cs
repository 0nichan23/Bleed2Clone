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
    [SerializeField] List<EnemySpawning> enemySpawnings;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        foreach (EnemySpawning enemySpawning in enemySpawnings)
        {
            for (int i = 0; i < enemySpawning.amount; i++)
            {
                enemySpawning.enemyDatabase.SpawnEnemy(transform);
                enemySpawning.enemyDatabase.weapon.Init();
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
                enemySpawning.enemyDatabase.weapon.BulletsPool.Clear();
            }
        }
    }
}
