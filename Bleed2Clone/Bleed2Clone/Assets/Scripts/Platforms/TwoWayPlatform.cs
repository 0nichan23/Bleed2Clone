using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TwoWayPlatform : MonoBehaviour
{
    [SerializeField] CompositeCollider2D platformCollider;
    [SerializeField] CapsuleCollider2D playerCollider;
    [SerializeField] LayerMask layerMask;
    private void Update()
    {
        // if the player holds shift make the platforms not collidable
        if (Input.GetKey(KeyCode.LeftShift))
        {
            platformCollider.isTrigger = true;
        }
        // if player is outside a collider we can make the platforms collidable again
        else if (!Physics2D.OverlapCapsule(playerCollider.bounds.center, playerCollider.size, playerCollider.direction, 0, layerMask))
        {
            platformCollider.isTrigger = false;
        }
        // if player stuck inside a platform release him
        if (Physics2D.OverlapCapsule(playerCollider.bounds.center, playerCollider.size / 2, playerCollider.direction, 0, layerMask))
        {
            platformCollider.isTrigger = true;
        }
    }
}
