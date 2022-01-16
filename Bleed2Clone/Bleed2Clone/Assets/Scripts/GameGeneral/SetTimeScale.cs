using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    [SerializeField] private float timeScale;
    void Start()
    {
        Time.timeScale = timeScale;
    }
}
