using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_YFollow : MonoBehaviour
{
    public Transform target;  // The target object to follow (main camera in this case)
    public float smoothSpeed = 0.125f;  // The smoothness of the camera movement
    public Vector3 offset;  // The offset distance between the camera and the target

    private void LateUpdate()
    {
        if (target == null)
        {
            // If the target is not assigned or is destroyed, do nothing
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        smoothedPosition.z = transform.position.z;  // Maintain the camera's original z position

        transform.position = smoothedPosition;
    }
}
