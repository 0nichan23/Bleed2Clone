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
                yield return new WaitForSeconds(0.5f);
            }

            enemy.agent.SetMovement(true);

            if (Vector2.Distance(enemy.transform.position, enemy.player.position) < 1)
            {
                enemy.agent.SetDestination(enemy.transform.position);
            }
            else
            {
                enemy.agent.SetDestination(new Vector2(enemy.player.position.x, enemy.transform.position.y));
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
