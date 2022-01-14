using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flying Enemy", menuName = "Enemies/Enemy Types/Flying Enemy")]
public class FlyingEnemy : EnemyDatabase
{
    public override IEnumerator OnCreated(Enemy enemy)
    {
        Debug.Log("On Created");
        yield return null;
    }

    public override IEnumerator PlayerInRangeBehaviour(Enemy enemy)
    {
        while (true)
        {
            if (Vector2.Distance(enemy.transform.position, enemy.player.position) < 1)
            {
                enemy.agent.SetDestination(enemy.transform.position);
            }
            else
            {
                float diff = (enemy.transform.position.x - enemy.player.position.x);
                enemy.agent.SetDestination(new Vector2(enemy.player.position.x + 3 * Mathf.Sign(diff), enemy.player.position.y + 3));
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
