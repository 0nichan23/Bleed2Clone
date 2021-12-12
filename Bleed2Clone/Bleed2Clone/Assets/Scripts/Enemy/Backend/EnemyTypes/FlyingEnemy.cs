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
        Debug.Log("Player In Range");
    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {
        Debug.Log("Player Not In Range");
    }
}
