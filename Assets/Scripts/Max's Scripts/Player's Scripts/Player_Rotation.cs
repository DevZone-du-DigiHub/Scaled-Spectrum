using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Rotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float angleOffset = 90f;

    [Header("Input Settings")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionReference leftStickAction;
    [SerializeField] private InputActionReference rightStickAction;

    private Vector2 currentLeftStickDirection = Vector2.zero;
    private Vector2 currentRightStickDirection = Vector2.zero;

    private void OnEnable()
    {
        if (leftStickAction != null)
        {
            leftStickAction.action.performed += ctx => SetLeftStickDirection(ctx.ReadValue<Vector2>());
            leftStickAction.action.canceled += ctx => SetLeftStickDirection(Vector2.zero);
            leftStickAction.action.Enable();
        }

        if (rightStickAction != null)
        {
            rightStickAction.action.performed += ctx => SetRightStickDirection(ctx.ReadValue<Vector2>());
            rightStickAction.action.canceled += ctx => SetRightStickDirection(Vector2.zero);
            rightStickAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (leftStickAction != null)
        {
            leftStickAction.action.performed -= ctx => SetLeftStickDirection(ctx.ReadValue<Vector2>());
            leftStickAction.action.canceled -= ctx => SetLeftStickDirection(Vector2.zero);
            leftStickAction.action.Disable();
        }

        if (rightStickAction != null)
        {
            rightStickAction.action.performed -= ctx => SetRightStickDirection(ctx.ReadValue<Vector2>());
            rightStickAction.action.canceled -= ctx => SetRightStickDirection(Vector2.zero);
            rightStickAction.action.Disable();
        }
    }

    void Update()
    {
        RotateTowardsInput();
    }

    void SetLeftStickDirection(Vector2 direction)
    {
        currentLeftStickDirection = direction;
    }

    void SetRightStickDirection(Vector2 direction)
    {
        currentRightStickDirection = direction;
    }

    void RotateTowardsInput()
    {
        Vector2 directionToUse = currentLeftStickDirection;

        // Check if there's right stick input, if yes then use it
        if (currentRightStickDirection.sqrMagnitude > 0.01)
        {
            directionToUse = currentRightStickDirection;
        }

        if (directionToUse.sqrMagnitude < 0.01) return;

        float targetAngle = Mathf.Atan2(directionToUse.y, directionToUse.x) * Mathf.Rad2Deg + angleOffset;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
