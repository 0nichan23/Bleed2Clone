using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TwoWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    [SerializeField] float waitTime =0.5f;
    float _waitTime;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            _waitTime = waitTime;
        }

        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (_waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                _waitTime = waitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            effector.rotationalOffset = 0;
        }
    }
}
