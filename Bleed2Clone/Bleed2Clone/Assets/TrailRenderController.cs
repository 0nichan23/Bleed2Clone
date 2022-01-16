using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRenderController : MonoBehaviour
{
    [SerializeField] private TrailRenderer TR;

    private void Start()
    {
        if (TR == null) TR = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        TR.Clear();   
    }
}
