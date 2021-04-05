using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    private float cameraZoomInDelay = 1.5f;
    private float timeElapsed;

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > cameraZoomInDelay)
        {
            offset = new Vector3(0, 30, -10);
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
