using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Following Enemy", menuName = "Enemies/Enemy Types/Following Enemy")]
public class FollowingEnemy : EnemyDatabase
{
    public override void OnCreated(Enemy enemy)
    {

    }

    public override void PlayerInRangeBehaviour(Enemy enemy)
    {

    }

    public override void PlayerNotInRangeBehaviour(Enemy enemy)
    {

    }
}
