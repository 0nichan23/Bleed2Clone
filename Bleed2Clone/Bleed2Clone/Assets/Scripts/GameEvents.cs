using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<bool> OnPlayerGroundedEvent;
    public static event Action<float, float, float> OnCameraShakeEvent;

    public static void OnPlayerGrounded(bool isGrounded)
    {
        OnPlayerGroundedEvent?.Invoke(isGrounded);
    }

    public static void OnCameraShake(float amplitude, float frequency, float duration)
    {
        OnCameraShakeEvent?.Invoke(amplitude, frequency, duration);
    }
}
