using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class WeaponDatabase : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public ObjectPool pool;

    public float cooldownTime;
    public float aimTime;
    public void Shoot(Weapon weapon)
    {

    }
}
