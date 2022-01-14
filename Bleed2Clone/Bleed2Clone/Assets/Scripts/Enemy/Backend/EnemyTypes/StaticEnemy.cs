using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Static Enemy", menuName = "Enemies/Enemy Types/Static Enemy")]
public class StaticEnemy : EnemyDatabase
{
    public override void OnCreated(Enemy enemy)
    {
        
    }

    public override void PlayerInRangeBehaviour(Enemy enemy)
    {
        enemy.weapon.Shoot();
    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {
        Debug.Log("Player Not In Range");
    }
}
