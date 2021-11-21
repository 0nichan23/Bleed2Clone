using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    Idle,
    Patrol,
    FollowPlayer,
    ShootPlayer,
    ShootPlayerStatic
}
public class EnemyAI : MonoBehaviour
{
    EnemyState state = EnemyState.Idle;
    void SwitchState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                IdleBehaviour();
                break;
            case EnemyState.Patrol:
                PatrolBehaviour();
                break;
            case EnemyState.FollowPlayer:
                FollowPlayerBehavior();
                break;
            case EnemyState.ShootPlayer:
                ShootPlayerBehavior();
                break;
            case EnemyState.ShootPlayerStatic:
                ShootPlayerStaticBehaviour();
                break;
        }
    }

    void IdleBehaviour() { }
    void PatrolBehaviour() { }
    void FollowPlayerBehavior() { }
    void ShootPlayerBehavior() { }
    void ShootPlayerStaticBehaviour() { }
}
