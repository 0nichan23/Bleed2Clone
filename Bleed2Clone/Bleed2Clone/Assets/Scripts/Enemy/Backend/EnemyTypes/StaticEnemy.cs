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
            enemy.weapon.Shoot();
            yield return new WaitForSeconds(stepsInSeconds);
        }
    }

    public override IEnumerator PlayerNotInRangeBehaviour(Enemy enemy)
    {
        Debug.Log("Player Not In Range");
        yield return null;
    }
}
