using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
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
    [SerializeField] private float meleeDamage;
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


    AudioSource audioSource;

    #region backend properties
    private float lastStrike;
    private float lastShot;
    private Coroutine lastShotArmAnimation;

    private ObjectPool pool;

    private Vector3 moveVector;
    internal Vector2 shootVector
    {
        get
        {
            if (playerController.usePCControls)
            {
                return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerController.transform.position).normalized;
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
        audioSource = GetComponent<AudioSource>();
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

            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
                if (EnemiesInMeleeRange.Length > 0)
                    Melee();
                else
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
            if (lastShotArmAnimation != null) StopCoroutine(lastShotArmAnimation);
            lastShotArmAnimation = StartCoroutine(ShootAnimation());
            bullet.transform.position = firePointTransform.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.transform.parent = null;
            bullet.SetActive(true);
            shot.direction = shootVector;

            AudioManager.instance.PlaySFX(audioSource, SFX_Type.shurikanShoot);
        }
    }

    void Melee()
    {
        if (Time.time - lastStrike < CoolDownMelee)
            return;

        lastStrike = Time.time;

        if (lastShotArmAnimation != null) StopCoroutine(lastShotArmAnimation);
        lastShotArmAnimation = StartCoroutine(ShootAnimation());

        AudioManager.instance.PlaySFX(audioSource, SFX_Type.melee);

        Collider2D[] HitEnemiesWithinHitBox = Physics2D.OverlapCircleAll(weaponTransform.position, attackRange, EnemyLayer);

        foreach (Collider2D enemy in HitEnemiesWithinHitBox)
        {
            StartCoroutine(enemy.GetComponent<Damagable>().TakeDamage(meleeDamage));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(weaponTransform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, MeleeModeRange);
    }

    private IEnumerator ShootAnimation()
    {
        shootArm.SetActive(false);
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
        float angle = Vector2.SignedAngle(new Vector2(xScale, 0), shootVector);
        shootArm.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}

