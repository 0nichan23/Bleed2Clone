using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform camTrans;
    [SerializeField] private float paraAmount;
    [SerializeField] private float BGWidth;
    [SerializeField] private bool initialized;
    [SerializeField] private float initialX;

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            DoParllax();
        }
    }

    private void DoParllax()
    {
        Vector3 camPos = camTrans.position;
        //The parallaxed background lags behind the camera by paraAmount
        float parallaxedPos = camPos.x * paraAmount;

        //The difference is the "vacant" space create between the background and the camera (its not really vacant thanks to us duplicating the background left and right)
        float difference = camPos.x * (1 - paraAmount);

        Vector3 newPos = new Vector3(initialX + parallaxedPos, camPos.y, 0);

        transform.position = newPos;

        //If the vacant space between the background and the camera is already the size of the background, shift the initial position to fill that vacant space back up
        if (difference > initialX + BGWidth)
            initialX += BGWidth;
        else if (difference < initialX - BGWidth)
            initialX -= BGWidth;
    }

    public void InitializeParallax(Transform cameraTransform, float parallaxAmount, float backgroundWidth)
    {
        camTrans = cameraTransform;
        paraAmount = parallaxAmount;
        BGWidth = backgroundWidth;
        initialX = transform.position.x;
        initialized = true;
    }
}
