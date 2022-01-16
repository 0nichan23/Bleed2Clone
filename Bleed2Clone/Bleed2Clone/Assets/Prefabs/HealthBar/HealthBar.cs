using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthBarSlider;
    Damagable damagable;
    private void Start()
    {
        healthBarSlider = transform.GetChild(0).GetComponentInChildren<Slider>();
        damagable= GetComponentInParent<Damagable>();
        //healthBarSlider.maxValue = damagable.GetMaxHP();

    }
    private void Update()
    {
        //healthBarSlider.value = damagable.GetCurrentHP();
    }

}
