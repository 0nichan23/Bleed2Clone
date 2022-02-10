using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class SlowTime : MonoBehaviour
{

    [Header("Slow Time")]
    public float TotalTime;
    public float curTime;
    bool slowingTime;
    public float TimeScale = 0.5f;
    public Image BarFill;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void Start()
    {
        curTime = TotalTime;
        //Bar.maxValue = TotalTime;
        BarFill.fillAmount = curTime / TotalTime;
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            StartSlowingTime();
        }
        if (Input.GetMouseButtonUp(1))
        {
            stopSlowingTime();
        }
        if (slowingTime)
        {
            Time.timeScale = TimeScale;
            curTime -= Time.deltaTime;
            if (curTime <= 0)
            {
                stopSlowingTime();
            }
        }
        else
        {
            Time.timeScale = 1;
            curTime += Time.deltaTime;
            if (curTime >= TotalTime)
            {
                curTime = TotalTime;
            }
        }
        BarFill.fillAmount = curTime / TotalTime;
    }

    void StartSlowingTime()
    {
        if (!slowingTime)
        {
            if (curTime > 0)
            {
                AudioManager.instance.PlaySFX(audioSource, SFX_Type.slowTime);
                slowingTime = true;
            }
        }
    }

    void stopSlowingTime()
    {
        AudioManager.instance.PlaySFX(audioSource, SFX_Type.stopSlowTime);
        slowingTime = false;
    }
}
