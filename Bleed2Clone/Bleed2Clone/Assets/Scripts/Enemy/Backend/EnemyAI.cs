using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEnemyStateChanger
{
    public void SwitchState(EnemyState state);
}
public class EnemyAI : IEnemyStateChanger
{
    [SerializeField] Enemy enemy;
    public void SwitchState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Created:
                enemy.OnCreated();
                break;
            case EnemyState.PlayerNotInRange:
                enemy.PlayerNotInRangeBehaviour();
                break;
            case EnemyState.PlayerInRange:
                enemy.PlayerInRangeBehaviour();
                break;
            case EnemyState.Death:
                enemy.Die();
                break;
        }
    }
}
