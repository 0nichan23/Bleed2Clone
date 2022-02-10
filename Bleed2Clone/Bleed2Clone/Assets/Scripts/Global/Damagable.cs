using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Damagable : MonoBehaviour
{
    [SerializeField] float maxHp;
    internal bool Dead;
    private float currentHp;

    [Header("Hit Effect")]

    [SerializeField] private bool onHitEffect;
    [SerializeField] private Transform gfxTransform;
    [SerializeField] private SpriteRenderer[] SRs;
    private Color myColor;
    AudioSource audioSource;

    private void Start()
    {
        currentHp = maxHp;
        myColor = SRs[0].color;
        audioSource = GetComponent<AudioSource>();
    }
    public IEnumerator TakeDamage(float howMuch)
    {
        //hit
        currentHp -= howMuch;
        if (GetComponent<PlayerController>() != null)
        {
            AudioManager.instance.PlaySFX(audioSource, SFX_Type.playerHit);
        }
        else
        {
            AudioManager.instance.PlaySFX(audioSource, SFX_Type.enemyHit);
        }

        //death
        if (currentHp <= 0)
        {
            if (GetComponent<PlayerController>() != null)
            {
                AudioManager.instance.PlaySFX(audioSource, SFX_Type.playerDeath);
                yield return new WaitForSeconds(audioSource.clip.length);
                transform.position = new Vector2(PlayerPrefs.GetFloat("saveX"), PlayerPrefs.GetFloat("saveY"));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (onHitEffect)
        {
            StartCoroutine(DoEffect());
        }
        yield return null;
    }
    public float GetCurrentHP()
    {
        return currentHp;
    }

    public void Heal()
    {
        currentHp = maxHp;
    }

    public float GetMaxHP()
    {
        return maxHp;
    }

    public void SetMaxHP(float _maxHP)
    {
        maxHp = _maxHP;
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
