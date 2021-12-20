using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Following Enemy", menuName = "Enemies/Enemy Types/Following Enemy")]
public class FollowingEnemy : EnemyDatabase
{
    public override void OnCreated(Enemy enemy)
    {
        Debug.Log("On Created");
    }

    public override void PlayerInRangeBehaviour(Enemy enemy)
    {
        Debug.Log("Player In Range");
    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {
        enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3, enemy.transform.position.y));
    }
}
