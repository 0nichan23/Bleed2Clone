using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponDatabase : ScriptableObject
{

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float chargeTime;

    public abstract void Shoot();
}
