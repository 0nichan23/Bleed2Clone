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
        shootVector = (enemy.player.transform.position- enemy.transform.position);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, shootVector);
    }
    public void Shoot()
    {
        if (Time.time - lastShot < CoolDownRanged)
        {
            return;
        }

        lastShot = Time.time;

        GameObject bullet = enemy.database.bulletPool.GetPooledObjects();

        if (bullet != null)
        {
            bullet.transform.position = firePointTransform.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = shootVector;
        }
    }
}
