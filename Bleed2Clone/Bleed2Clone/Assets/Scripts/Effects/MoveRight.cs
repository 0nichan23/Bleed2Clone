using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    //This is a quick script to move the camera in the main menu for visual effect

    [SerializeField] private float moveRate;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveRate * Time.deltaTime);
    }
}
