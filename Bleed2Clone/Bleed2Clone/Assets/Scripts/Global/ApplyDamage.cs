using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    [SerializeField] float damage;
    
    public void ApplyDamageToDamagable(Damagable damagable)
    {
        StartCoroutine(damagable.TakeDamage(damage));
    }
}
