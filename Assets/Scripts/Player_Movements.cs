using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movements : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 10.0f;
    [SerializeField] private float backwardSpeed = 5.0f;
    [SerializeField] private float sidewaysSpeed = 7.0f;

    [Header("Input Settings")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference rightStickAction;

    [Header("Camera Settings")]
    [SerializeField] private Transform mainCameraTransform;

    [Header("Animator Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string yAnimParam = "L-Joy-Y_anim";
    [SerializeField] private string xAnimParam = "L-Joy-X_anim";

    private Rigidbody2D rb;
    private Vector2 currentMovement = Vector2.zero;
    private bool isRightStickActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        movementAction.action.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        movementAction.action.canceled += ctx => SetMovement(Vector2.zero);

        rightStickAction.action.performed += ctx => isRightStickActive = true;
        rightStickAction.action.canceled += ctx => isRightStickActive = false;

        movementAction.action.Enable();
        rightStickAction.action.Enable();
    }

    private void OnDisable()
    {
        movementAction.action.performed -= ctx => SetMovement(ctx.ReadValue<Vector2>());
        movementAction.action.canceled -= ctx => SetMovement(Vector2.zero);

        rightStickAction.action.performed -= ctx => isRightStickActive = true;
        rightStickAction.action.canceled -= ctx => isRightStickActive = false;

        movementAction.action.Disable();
        rightStickAction.action.Disable();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void Update()
    {
        UpdateAnimatorParameters();
    }

    void SetMovement(Vector2 movement)
    {
        currentMovement = movement;
    }

    void MoveCharacter()
    {
        Vector2 desiredDirection = (mainCameraTransform.up * currentMovement.y + mainCameraTransform.right * currentMovement.x).normalized;
        float angleToForward = Vector2.Angle(transform.up, desiredDirection);

        float speedMultiplier;
        if (isRightStickActive)
        {
            if (angleToForward < 45) // Mostly forward
                speedMultiplier = forwardSpeed;
            else if (angleToForward > 135) // Mostly backward
                speedMultiplier = backwardSpeed;
            else // Mostly sideways
                speedMultiplier = sidewaysSpeed;

            rb.velocity = desiredDirection * speedMultiplier;
        }
        else
        {
            rb.velocity = desiredDirection * forwardSpeed; // Use forward speed when the right stick isn't active
        }
    }

    void UpdateAnimatorParameters()
    {
        if (animator)
        {
            animator.SetFloat(xAnimParam, currentMovement.x);
            animator.SetFloat(yAnimParam, currentMovement.y);
        }
    }
}
