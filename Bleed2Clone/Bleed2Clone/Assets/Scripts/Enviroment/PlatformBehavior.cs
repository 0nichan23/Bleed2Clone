using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{

    private PlatformEffector2D effector;
    public BoxCollider2D platform;

    public float waitTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0.2f;
            platform.enabled = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (waitTime <= 0)
            {
                //effector.rotationalOffset = 180;
                waitTime = 0.2f;
                platform.enabled = false;
            }
            else waitTime -= Time.deltaTime;
        }

        //effector.surfaceArc = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //platform.isTrigger = false;
            //effector.rotationalOffset = 0;
        }

        //if (Input.GetKey(KeyCode.UpArrow))
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("enter");
                collision.gameObject.transform.SetParent(transform);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                effector.rotationalOffset = 0;
                Debug.Log("exit");
                collision.gameObject.transform.SetParent(null);
                platform.enabled = true;
            }
        }
    }
}

