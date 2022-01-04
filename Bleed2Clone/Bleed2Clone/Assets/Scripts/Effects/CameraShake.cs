using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.OnCameraShakeEvent += ShakeCamera;
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShakeCamera(float amplitude, float frequency, float duration)
    {
        StopAllCoroutines();
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
        StartCoroutine(ActivateShake(amplitude, frequency, duration));
    }

    private IEnumerator ActivateShake(float amplitude, float frequency, float duration)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;

        float maxDuration = duration;

        while(duration > 0)
        {
            float lerpAmount = duration / maxDuration;
            noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, amplitude, lerpAmount);
            noise.m_FrequencyGain = Mathf.Lerp(noise.m_FrequencyGain, frequency, lerpAmount);

            duration -= Time.deltaTime;
            yield return null;
        }

        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}
