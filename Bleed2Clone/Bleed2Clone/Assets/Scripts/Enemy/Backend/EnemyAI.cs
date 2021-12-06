using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEnemyStateChanger
{
    public void SwitchState(Enemy enemy);
}
public class EnemyAI : IEnemyStateChanger
{
    private Enemy _enemy;

    public EnemyAI(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void SwitchState(Enemy enemy)
    {
        switch (enemy.state)
        {
            case EnemyState.Created:
                break;
            case EnemyState.PlayerNotInRange:
                break;
            case EnemyState.PlayerInRange:
                break;
            case EnemyState.Death:

                break;
        }
    }
}
