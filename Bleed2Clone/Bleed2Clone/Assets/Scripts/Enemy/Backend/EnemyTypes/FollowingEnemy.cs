using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Following Enemy", menuName = "Enemies/Enemy Types/Following Enemy")]
public class FollowingEnemy : EnemyDatabase
{
    public LayerMask groundLayer;
    public override void OnCreated(Enemy enemy)
    {
        Debug.Log("On Created");
    }

    public override void PlayerInRangeBehaviour(Enemy enemy)
    {
        enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3, enemy.player.position.y));
    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {
        enemy.agent.SetDestination(enemy.lastGroundedPos);
    }
}
