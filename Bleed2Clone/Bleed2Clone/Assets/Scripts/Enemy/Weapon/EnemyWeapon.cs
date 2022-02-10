using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EnemyWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField] private Transform firePointTransform;
    [SerializeField] private float CoolDownRanged;
    AudioSource audioSource;
    #region backend properties
    private float lastShot;
    private Vector2 shootVector;
    internal Enemy enemy;
    #endregion
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void Update()
    {
        if (enemy.player != null)
        {
            shootVector = (enemy.player.transform.position - enemy.transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
        }
    }
    public static bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }

    public void Shoot()
    {
        if (IsVisibleToCamera(transform))
        {
            GameObject bullet = enemy.database.bulletPool.GetPooledObjects();

            if (bullet != null)
            {
                AudioManager.instance.PlaySFX(audioSource, SFX_Type.fireBulletShoot);

                bullet.transform.position = firePointTransform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, shootVector) - 90f);
                Bullet shot = bullet.GetComponent<Bullet>();
                bullet.SetActive(true);
                shot.direction = shootVector;
            }
        }
    }
}
