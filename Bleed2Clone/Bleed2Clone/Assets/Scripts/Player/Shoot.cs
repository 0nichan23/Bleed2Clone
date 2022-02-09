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
    [SerializeField] private Transform playerGFX; //using this to check if the player is flipped
    PlayerController playerController;

    #region backend properties
    private float lastStrike;
    private float lastShot;

    private ObjectPool pool;

    private Vector3 moveVector;
    internal Vector2 shootVector
    {
        get
        {
            if (playerController.usePCControls)
            {
                return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
            else
            {
                return (Vector2.up * moveJoystick.Vertical - Vector2.left * moveJoystick.Horizontal);
            }
        }
    }
    #endregion

    private void Start()
    {
        pool = GetComponentInChildren<ObjectPool>();
        pool.Init();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        EnemiesInMeleeRange = Physics2D.OverlapCircleAll(transform.position, MeleeModeRange, EnemyLayer);

        if (!playerController.usePCControls)
        {
            if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
                //if (EnemiesInMeleeRange.Length > 0)
                //{
                //    Melee();
                //}
                //else
                //{
                Pew();
                //  }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
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
            StopCoroutine(ShootAnimation());
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

        SetArmAngle();

        yield return new WaitForSeconds(0.3f);

        shootArm.SetActive(false);
        defaultArm.SetActive(true);

    }

    private void SetArmAngle()
    {
        float xScale = Mathf.Sign(playerGFX.localScale.x);
        float angle = Vector2.SignedAngle(new Vector2(xScale, 0), moveJoystick.Direction);
        shootArm.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}

