using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLogic : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform directFollowTarget;
    [SerializeField] private Transform joystickFollowTarget;

    [Header("Zoom Settings")]
    [SerializeField] private float idleZoom = 5f;
    [SerializeField] private float maxZoom = 10f;

    [Header("Tag Settings")]
    [SerializeField] private string followTag = "player/core";

    [Header("Camera Control")]
    [SerializeField] private bool allowCameraControl = true;

    [Header("Smooth Follow")]
    [SerializeField] private float smoothFollowTime = 0.2f;

    [Header("Camera Distance Limits")]
    [SerializeField] private float minCameraDistance = 5f; // Minimum distance from player
    [SerializeField] private float maxCameraDistance = 10f; // Maximum distance from player

    [Header("Input Action")]
    [SerializeField] private InputActionReference cameraControlAction;

    private Camera mainCamera;
    private Transform currentTarget;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        cameraControlAction.action.Enable();
    }

    private void LateUpdate()
    {
        HandleCameraFollow();
        HandleSmoothFollow();
    }

    private void HandleCameraFollow()
    {
        if (!allowCameraControl)
        {
            SmoothlyTransitionToTarget(directFollowTarget);
            mainCamera.orthographicSize = idleZoom;
            return;
        }

        Vector2 rightJoystickInput = cameraControlAction.action.ReadValue<Vector2>();

        if (rightJoystickInput == Vector2.zero)
        {
            SmoothlyTransitionToTarget(directFollowTarget);
            mainCamera.orthographicSize = idleZoom;
        }
        else
        {
            SmoothlyTransitionToTarget(joystickFollowTarget);
            float targetZoom = Mathf.Lerp(idleZoom, maxZoom, rightJoystickInput.magnitude);
            mainCamera.orthographicSize = targetZoom;

            // Adjust camera distance based on joystick input
            float targetDistance = Mathf.Lerp(minCameraDistance, maxCameraDistance, rightJoystickInput.magnitude);
            Vector3 offset = (targetPosition - joystickFollowTarget.position).normalized * targetDistance;
            targetPosition = joystickFollowTarget.position + offset;
        }
    }

    private void HandleSmoothFollow()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothFollowTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(followTag))
        {
            // Handle OnTriggerExit logic
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(followTag))
        {
            // Handle OnTriggerEnter logic
        }
    }

    private void SmoothlyTransitionToTarget(Transform newTarget)
    {
        if (currentTarget == newTarget)
        {
            targetPosition = currentTarget.position;
            return;
        }

        currentTarget = newTarget;
        targetPosition = newTarget.position;
    }
}
