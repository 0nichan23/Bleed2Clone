using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] private FixedJoystick moveJoystick;

    [Header("Enemies")]
    [SerializeField] private LayerMask EnemyLayer;

    [Header("Attack")]
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform firePointTransform;

    [Header("Melee")]
    [SerializeField] private float MeleeModeRange;
    [SerializeField] private Collider2D[] EnemiesInMeleeRange;

    [Header("Cooldown")]
    [SerializeField] private float CoolDownMelee;
    [SerializeField] private float CoolDownRanged;

    [Header("Arms")]

    [SerializeField] private GameObject defaultArm;
    [SerializeField] private GameObject shootArm;

    #region backend properties
    private float lastStrike;
    private float lastShot;

    private ObjectPool pool;

    private Vector3 moveVector;
    internal Vector2 shootVector;
    #endregion

    private void Start()
    {
        pool = GetComponentInChildren<ObjectPool>();
        pool.Init();
    }
    private void Update()
    {
        shootVector = (Vector2.up * moveJoystick.Vertical - Vector2.left * moveJoystick.Horizontal);
        EnemiesInMeleeRange = Physics2D.OverlapCircleAll(transform.position, MeleeModeRange, EnemyLayer);

        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
            if (EnemiesInMeleeRange.Length > 0)
            {
                Melee();
            }
            else
            {
                Pew();
            }
        }

    }

    void Pew()
    {
        if (Time.time - lastShot < CoolDownRanged)
        {
            return;
        }

        lastShot = Time.time;

        GameObject bullet = pool.GetPooledObjects();

        if (bullet != null)
        {
            StartCoroutine(ShootAnimation());
            bullet.transform.position = firePointTransform.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.transform.parent = null;
            bullet.SetActive(true);
            shot.direction = shootVector;
        }
    }

    void Melee()
    {
        if (Time.time - lastStrike < CoolDownMelee)
            return;

        lastStrike = Time.time;

        Collider2D[] HitEnemiesWithinHitBox = Physics2D.OverlapCircleAll(weaponTransform.position, attackRange, EnemyLayer);

        foreach (Collider2D enemy in HitEnemiesWithinHitBox)
            Debug.Log("hit " + enemy.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(weaponTransform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, MeleeModeRange);
    }

    private IEnumerator ShootAnimation()
    {
        shootArm.SetActive(true);
        defaultArm.SetActive(false);

        float angle = Vector2.Angle(Vector2.right, moveJoystick.Direction);
        if (moveJoystick.Vertical < 0) angle *= -1;
        shootArm.transform.rotation = Quaternion.Euler(Vector3.zero);
        shootArm.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(0.4f);

        shootArm.SetActive(false);
        defaultArm.SetActive(true);

    }

}

