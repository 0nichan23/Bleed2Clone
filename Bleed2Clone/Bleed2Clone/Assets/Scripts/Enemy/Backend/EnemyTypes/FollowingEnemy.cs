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
        float Move = stepsInSeconds;

        while (true)
        {
            Move -= Time.deltaTime;
            if (Move <= 0)
            {
                Move = stepsInSeconds;
                enemy.agent.SetMovement(false);
                enemy.weapon.Shoot();
                yield return new WaitForSeconds(cooldown);
            }

            enemy.agent.SetMovement(true);

            destination = new Vector2(enemy.player.position.x, enemy.transform.position.y);

            if (enemy.player != null && Vector2.Distance(enemy.transform.position, enemy.player.position) < 1)
            {
                enemy.agent.SetDestination(enemy.transform.position);
            }
            else if (enemy.player != null && !isDestinationInGround(enemy, destination))
            {
                enemy.agent.SetDestination(destination);
            }
            else
            {
                enemy.agent.SetDestination(enemy.transform.position);
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
