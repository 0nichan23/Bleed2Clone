using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarFill;
    Damagable damagable;
    private void Start()
    {
        damagable = GetComponentInParent<Damagable>();
    }
    private void Update()
    {
        float fill = damagable.GetCurrentHP() / damagable.GetMaxHP();
        healthBarFill.fillAmount = fill;
    }

}
