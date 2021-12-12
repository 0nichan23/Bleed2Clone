using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public int StaticEnemiesAmount;
    public int FlyingEnemiesAmount;
    public int FollowingEnemiesAmount;

    public StaticEnemy staticEnemyDatabase;
    public FlyingEnemy flyingEnemyDatabase;
    public FollowingEnemy followingEnemyDatabase;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < StaticEnemiesAmount; i++)
        {
            staticEnemyDatabase.SpawenEnemy(transform);
        }
        for (int i = 0; i < FlyingEnemiesAmount; i++)
        {
            flyingEnemyDatabase.SpawenEnemy(transform);
        }
        for (int i = 0; i < FollowingEnemiesAmount; i++)
        {
            followingEnemyDatabase.SpawenEnemy(transform);
        }
    }
}
