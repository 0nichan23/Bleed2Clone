using UnityEngine;
using UnityEngine.UI;

public class SlowTime : MonoBehaviour
{

    [Header("Slow Time")]
    public float TotalTime;
    public float curTime;
    bool slowingTime;
    public float TimeScale = 0.5f;
    public Slider Bar;

    private void Start()
    {
        curTime = TotalTime;
        Bar.maxValue = TotalTime;
        Bar.value = curTime;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartSlowingTime();
        }
        if (Input.GetMouseButtonUp(0))
        {
            stopSlowingTime();
        }
        if (slowingTime)
        {
            Time.timeScale = TimeScale;
            curTime -= Time.deltaTime;
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
        Bar.value = curTime;
    }

    void StartSlowingTime()
    {
        if (!slowingTime)
        {
            if (curTime > 0)
            {
                slowingTime = true;
            }
        }
    }

    void stopSlowingTime()
    {
        slowingTime = false;
    }
}
