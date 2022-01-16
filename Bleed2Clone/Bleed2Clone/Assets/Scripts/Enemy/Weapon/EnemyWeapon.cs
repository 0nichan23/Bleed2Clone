using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField] private Transform firePointTransform;
    [SerializeField] private float CoolDownRanged;

    #region backend properties
    private float lastShot;
    private Vector2 shootVector;
    internal Enemy enemy;
    #endregion
    private void Update()
    {
        if (enemy.player != null)
        {
            shootVector = (enemy.player.transform.position - enemy.transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
        }
    }
    public void Shoot()
    {
        GameObject bullet = enemy.database.bulletPool.GetPooledObjects();

        if (bullet != null)
        {
            bullet.transform.position = firePointTransform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, shootVector) - 90f);
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = shootVector;
        }
    }
}
