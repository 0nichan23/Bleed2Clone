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
            float diff = (enemy.transform.position.x - enemy.player.position.x);
            enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3 * Mathf.Sign(diff), enemy.transform.position.y));
            enemy.weapon.Shoot();
            yield return null;
        }
    }

    public override IEnumerator PlayerNotInRangeBehaviour(Enemy enemy)
    {
        enemy.agent.SetDestination(enemy.agent.lastGroundedPos);
        yield return null;
    }
}
