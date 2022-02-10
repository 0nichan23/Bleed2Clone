using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public Transform weaponTransform;
    internal GameObject weaponObject;
    internal EnemyWeapon weapon;
    internal Agent agent;
    internal Transform player;

    private float hp;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private EnemyState _state = EnemyState.Created;

    internal EnemyDatabase database;
    internal AudioSource audioSource;
    private void Start()
    {
        if (FindObjectOfType<PlayerController>())
            player = FindObjectOfType<PlayerController>().gameObject.transform;

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponentInChildren<SpriteRenderer>();
        hp = database.MaxHP;
        agent = GetComponent<Agent>();

        weaponObject = Instantiate(database.weaponPrefab, weaponTransform.position, Quaternion.identity, weaponTransform);
        weapon = weaponObject.GetComponent<EnemyWeapon>();
        weapon.enemy = this;
    }
    private void Update()
    {
        if (player != null)
        {
            if (DistanceFromPlayer() <= database.enemyRange)
            {
                SwitchState(EnemyState.PlayerInRange);
            }
            else
            {
                SwitchState(EnemyState.PlayerNotInRange);
            }

            Vector3 enemyDirectionLocal = player.transform.InverseTransformPoint(transform.position);
            sr.flipX = enemyDirectionLocal.x < 0;
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

        StopAllCoroutines();
        switch (_state)
        {
            case EnemyState.PlayerNotInRange:
                StartCoroutine(database.PlayerNotInRangeBehaviour(this));
                break;
            case EnemyState.PlayerInRange:
                StartCoroutine(database.PlayerInRangeBehaviour(this));
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
    private void OnDestroy()
    {
        database.activeEnemiesFromThisType.Remove(this);
    }
}
