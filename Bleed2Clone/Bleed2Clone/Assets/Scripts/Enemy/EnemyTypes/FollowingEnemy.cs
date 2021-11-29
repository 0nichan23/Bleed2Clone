using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy
{
    public override void Die()
    {
        Destroy(enemyPrefab);

    }

    public override void OnCreated()
    {
        throw new System.NotImplementedException();
    }

    public override void PlayerInRangeBehaviour()
    {
        throw new System.NotImplementedException();
    }

    public override void PlayerNotInRangeBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
