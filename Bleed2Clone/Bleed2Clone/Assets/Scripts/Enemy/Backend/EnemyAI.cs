using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEnemyStateChanger
{
    public void SwitchState(Enemy enemy);
}
public class EnemyAI : MonoBehaviour, IEnemyStateChanger
{
    public void SwitchState(Enemy enemy)
    {
        switch (enemy.state)
        {
            case EnemyState.Created:
                enemy.OnCreated();
                break;
            case EnemyState.Idle:
                enemy.IdleBehaviour();
                break;
            case EnemyState.Patrol:
                enemy.PatrolBehaviour();
                break;
            case EnemyState.FollowPlayer:
                enemy.FollowPlayerBehavior();
                break;
            case EnemyState.ShootPlayer:
                enemy.ShootBehavior();
                break;
            case EnemyState.Death:
                enemy.Die();
                break;
        }
    }
}
