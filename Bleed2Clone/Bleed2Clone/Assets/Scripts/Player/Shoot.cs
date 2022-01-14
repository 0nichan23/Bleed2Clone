using UnityEngine;

public class Shoot : MonoBehaviour
{
    public FixedJoystick joystick;
    public Transform gunz;
    public LayerMask EnemyLayer;
    public Transform AttackPoint;
    public float attackRange;
    public float MeleeModeRange;
    Collider2D[] EnemiesInMeleeRange;
    public float CoolDownMelee;
    float lastStrike;
    public float CoolDownRanged;
    float lastShot;
    public ObjectPool pool;
    public Transform BulletPos;
    Vector3 moveVector;
    public static Vector2 shootVector;
    private void Update()
    {
        moveVector = (Vector3.up * joystick.Vertical - Vector3.left * joystick.Horizontal);
        shootVector = (Vector2.up * joystick.Vertical - Vector2.left * joystick.Horizontal);
        EnemiesInMeleeRange = Physics2D.OverlapCircleAll(transform.position, MeleeModeRange, EnemyLayer);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
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
        /*RaycastHit2D hit = Physics2D.Raycast(gunz.position, gunz.up, EnemyLayer);
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.layer == EnemyLayer)
            {
                Debug.Log(hit.transform.name);
            }
        }*/
        if (Time.time - lastShot < CoolDownRanged)
        {
            return;
        }
        lastShot = Time.time;

        GameObject bullet = pool.GetPooledObjects();
        if (bullet != null)
        {
            bullet.transform.position = BulletPos.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
        }

    }

    void Melee()
    {
        if (Time.time - lastStrike < CoolDownMelee)
        {
            return;
        }
        lastStrike = Time.time;
        Collider2D[] HitEnemiesWithinHitBox = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, EnemyLayer);
        foreach (Collider2D enemy in HitEnemiesWithinHitBox)
        {
            Debug.Log("hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, MeleeModeRange);
    }
}
