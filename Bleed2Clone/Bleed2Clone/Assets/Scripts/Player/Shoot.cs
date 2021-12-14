using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public FixedJoystick joystick;
    public Transform gunz;

    private void Update()
    {
        Vector3 moveVector = (Vector3.up * joystick.Vertical - Vector3.left * joystick.Horizontal);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            Pew();
        }

    }

    void Pew()
    {
        RaycastHit2D hit = Physics2D.Raycast(gunz.position, gunz.right);
        if (hit)
        {
            Debug.Log(hit.transform.name);
        }
    }
}
