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
        if (enemy.isGrounded)
            Debug.Log("attack");
        else
        {
            enemy.agent.SetDestination(enemy.lastGroundedPos);
        }
    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {
        if (enemy.isGrounded)
            enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3, enemy.transform.position.y));
        else
            enemy.agent.SetDestination(enemy.lastGroundedPos);
    }
}
