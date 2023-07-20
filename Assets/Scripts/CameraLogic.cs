using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float followRadius = 5f;
    [SerializeField] private float exitColliderFollowRadius = 10f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float zoomSpeed = 2f;

    private Vector3 targetPosition;
    private Camera mainCamera;
    private bool isFollowing = true;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the target position with the appropriate follow radius
        float currentFollowRadius = isFollowing ? followRadius : exitColliderFollowRadius;
        Vector3 targetFlatPosition = target.position;
        targetFlatPosition.z = transform.position.z; // Keep the same Z position as the camera
        Vector3 offset = targetFlatPosition - transform.position;
        if (offset.magnitude > currentFollowRadius)
        {
            targetPosition = transform.position + offset.normalized * currentFollowRadius;
        }

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Calculate the zoom based on distance from player
        float distance = offset.magnitude;
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, distance / currentFollowRadius);

        // Smoothly adjust the camera's orthographic size
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }
}
