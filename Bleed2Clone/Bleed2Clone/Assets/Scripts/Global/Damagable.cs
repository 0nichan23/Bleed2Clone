using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] float maxHp;
    private float currentHp;

    [Header("Hit Effect")]

    [SerializeField] private bool onHitEffect;
    [SerializeField] private SpriteRenderer graphics;
    private Color myColor;

    private void Start()
    {
        currentHp = maxHp;
        myColor = graphics.color;
    }
    public void TakeDamage(float howMuch)
    {
        currentHp -= howMuch;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }

        if (onHitEffect) StartCoroutine(DoEffect());

    }
    public float GetCurrentHP()
    {
        return currentHp;
    }
    public float GetMaxHP()
    {
        return maxHp;
    }

    private IEnumerator DoEffect()
    {
        graphics.transform.localScale = new Vector3(0.8f, 1.2f, 1f);
        Color currentColor = myColor;
        graphics.color = Color.red;
        float lerpAmount = 0f;

        while (lerpAmount < 1f)
        {
            lerpAmount += (0.1f * Time.deltaTime);
            if (lerpAmount > 1f) lerpAmount = 1f;
            graphics.transform.localScale = Vector3.Lerp(graphics.transform.localScale, Vector3.one, lerpAmount);
            graphics.color = Color.Lerp(graphics.color, currentColor, lerpAmount);

            yield return null;
        }

    }
}
