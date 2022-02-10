using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SlowTime : MonoBehaviour
{

    [Header("Slow Time")]
    [SerializeField] private UnityEngine.Rendering.Volume slowtimeVolume;
    private float volumeLerp = 0;
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

            if (volumeLerp < 1) volumeLerp += Time.deltaTime * 2f;
        }
        else
        {
            Time.timeScale = 1;
            curTime += Time.deltaTime;
            if (curTime >= TotalTime)
            {
                curTime = TotalTime;
            }

            if (volumeLerp > 0) volumeLerp -= Time.deltaTime * 2f;
        }
        BarFill.fillAmount = curTime / TotalTime;

        slowtimeVolume.weight = volumeLerp;
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
