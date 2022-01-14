using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class WeaponDatabase : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public int InitBulletsInPool;
    public List<GameObject> BulletsPool;

    public float cooldownTime;
    public float aimTime;

    public void Init()
    {
        for (int i = 0; i < InitBulletsInPool; i++)
        {
            GameObject temp = Instantiate(bulletPrefab);
            BulletsPool.Add(temp);
            temp.SetActive(false);
        }
    }
    public void Shoot(Weapon weapon)
    {

    }
}
