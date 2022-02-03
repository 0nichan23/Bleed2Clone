using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] float maxHp;
    private float currentHp;

    [Header("Hit Effect")]

    [SerializeField] private bool onHitEffect;
    [SerializeField] private Transform gfxTransform;
    [SerializeField] private SpriteRenderer[] SRs;
    private Color myColor;

    private void Start()
    {
        currentHp = maxHp;
        myColor = SRs[0].color;
    }
    public void TakeDamage(float howMuch)
    {
        currentHp -= howMuch;
        if (currentHp <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
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
        gfxTransform.localScale = new Vector3(0.8f, 1.2f, 1f);
        Color currentColor = myColor;

        foreach (SpriteRenderer sr in SRs)
            sr.color = Color.red;

        float lerpAmount = 0f;
        Color startingColor = Color.red;
        Vector3 startingScale = gfxTransform.localScale;

        while (lerpAmount < 1f)
        {
            lerpAmount += 2f * Time.deltaTime;
            //print("lerp amount: " + lerpAmount);
            gfxTransform.localScale = Vector3.Lerp(startingScale, Vector3.one, lerpAmount);

            foreach (SpriteRenderer sr in SRs)
                sr.color = Color.Lerp(startingColor, currentColor, lerpAmount);

            yield return null;
        }

        //print("finished on hit effect");
    }
}
