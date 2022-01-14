using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Following Enemy", menuName = "Enemies/Enemy Types/Following Enemy")]
public class FollowingEnemy : EnemyDatabase
{
    public LayerMask groundLayer;
    public override IEnumerator OnCreated(Enemy enemy)
    {
        Debug.Log("On Created");
        yield return null;
    }

    public override IEnumerator PlayerInRangeBehaviour(Enemy enemy)
    {
        while (true)
        {
            enemy.weapon.Shoot();
            if (Vector2.Distance(enemy.transform.position, enemy.player.position) < 1)
            {
                enemy.agent.SetDestination(enemy.transform.position);
            }
            else
            {
                float diff = (enemy.transform.position.x - enemy.player.position.x);
                enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3 * Mathf.Sign(diff), enemy.transform.position.y));
            }
            if (Vector2.Distance(enemy.transform.position, enemy.player.position) < enemyShootRange)
            {
                enemy.weapon.Shoot();
            }
            yield return null;
        }
    }

    public override IEnumerator PlayerNotInRangeBehaviour(Enemy enemy)
    {
        enemy.agent.SetDestination(enemy.agent.lastGroundedPos);
        yield return null;
    }
}
