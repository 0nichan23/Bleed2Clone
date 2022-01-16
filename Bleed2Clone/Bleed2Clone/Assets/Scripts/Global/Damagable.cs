using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] float maxHp;
    private float currentHp;

    private void Start()
    {
        currentHp = maxHp;
    }
    public void TakeDamage(float howMuch)
    {
        currentHp -= howMuch;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
