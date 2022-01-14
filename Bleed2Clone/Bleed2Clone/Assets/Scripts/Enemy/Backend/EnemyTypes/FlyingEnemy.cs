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

            if (Vector2.Distance(enemy.transform.position, enemy.player.position) < 1)
            {
                enemy.agent.SetDestination(enemy.transform.position);
            }
            else
            {
                enemy.agent.SetDestination(new Vector2(enemy.player.position.x, enemy.player.position.y + 3));
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
