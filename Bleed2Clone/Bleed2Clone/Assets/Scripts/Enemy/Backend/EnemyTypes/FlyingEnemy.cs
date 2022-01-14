using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flying Enemy", menuName = "Enemies/Enemy Types/Flying Enemy")]
public class FlyingEnemy : EnemyDatabase
{
    public override void OnCreated(Enemy enemy)
    {
        Debug.Log("On Created");
    }

    public override void PlayerInRangeBehaviour(Enemy enemy)
    {
        float diff = (enemy.transform.position.x - enemy.player.position.x);
        enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3 * Mathf.Sign(diff), enemy.player.position.y + 3));
        enemy.weapon.Shoot();
    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {
        enemy.agent.SetDestination(enemy.agent.lastGroundedPos);
    }
}
