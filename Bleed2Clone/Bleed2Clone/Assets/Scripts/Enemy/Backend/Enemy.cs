using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Transform weaponTransform;
    internal GameObject weaponObject;
    internal EnemyWeapon weapon;
    internal Agent agent;
    internal Transform player;

    private float hp;
    private Rigidbody2D rb;

    private EnemyState _state = EnemyState.Created;

    internal EnemyDatabase database;

    private void Start()
    {
        if (FindObjectOfType<PlayerController>())
            player = FindObjectOfType<PlayerController>().gameObject.transform;

        rb = GetComponent<Rigidbody2D>();
        hp = database.MaxHP;
        agent = GetComponent<Agent>();

        weaponObject = Instantiate(database.weaponPrefab, weaponTransform.position, Quaternion.identity, weaponTransform);
        weapon = weaponObject.GetComponent<EnemyWeapon>();
        weapon.enemy = this;
    }
    private void Update()
    {
        if (player != null)
            if (DistanceFromPlayer() <= database.enemyRange)
            {
                SwitchState(EnemyState.PlayerInRange);
            }
            else
            {
                SwitchState(EnemyState.PlayerNotInRange);
            }
    }
    private float DistanceFromPlayer()
    {
        return Vector2.Distance(this.transform.position, player.position);
    }
    public bool SwitchState(EnemyState state)
    {
        if (state != _state)
            _state = state;
        else return false;

        switch (_state)
        {
            case EnemyState.PlayerNotInRange:
                database.PlayerNotInRangeBehaviour(this);
                break;
            case EnemyState.PlayerInRange:
                database.PlayerInRangeBehaviour(this);
                break;
            case EnemyState.Death:
                database.OnDeath(this);
                break;
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (agent.IsGrounded)
        {
            rb.isKinematic = true;
            agent.SetDestination(agent.lastGroundedPos);
            agent.SetMovement(true);
        }
    }
}
