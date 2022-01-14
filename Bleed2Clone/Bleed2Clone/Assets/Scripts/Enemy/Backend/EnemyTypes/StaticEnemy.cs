using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Static Enemy", menuName = "Enemies/Enemy Types/Static Enemy")]
public class StaticEnemy : EnemyDatabase
{
    public override IEnumerator OnCreated(Enemy enemy)
    {
        yield return null;
    }

    public override IEnumerator PlayerInRangeBehaviour(Enemy enemy)
    {
        while (true)
        {
            if (Vector2.Distance(enemy.transform.position, enemy.player.position) < enemyShootRange)
            {
                enemy.weapon.Shoot();
            }
            yield return null;
        }
    }

    public override IEnumerator PlayerNotInRangeBehaviour(Enemy enemy)
    {
        Debug.Log("Player Not In Range");
        yield return null;
    }
}
