using UnityEngine;
using UnityEngine.InputSystem;

public class Rotation_Inputs : MonoBehaviour
{
    [Header("Customizable Settings")]
    [Tooltip("Speed at which the object rotates in degrees per second.")]
    public float rotationSpeed = 180f;

    [Tooltip("Deadzone for the analog stick input. Input values below this magnitude will be ignored.")]
    public float deadzone = 0.1f;

    [Tooltip("Threshold value to determine if the analog stick input has changed. Input values must differ by at least this amount to trigger rotation.")]
    public float inputThreshold = 0.01f;

    private Quaternion initialRotation;
    private int _currentGamepadID = -1;

    private void Start()
    {
        // Check if a Gamepad is connected and get its ID
        if (Gamepad.current != null)
        {
            _currentGamepadID = Gamepad.current.deviceId;
        }

        // Store the initial rotation of the object
        initialRotation = transform.rotation;

        // Enable analog stick input if a Gamepad is connected
        if (_currentGamepadID != -1)
        {
            EnableAnalogStickInput();
        }
    }

    private void Update()
    {
        // Check if the connected Gamepad has changed
        if (Gamepad.current != null)
        {
            if (_currentGamepadID != Gamepad.current.deviceId)
            {
                // Disable the previous Gamepad and enable the new one
                _currentGamepadID = Gamepad.current.deviceId;
                EnableAnalogStickInput();
            }
        }
        else
        {
            // No Gamepad connected, reset the Gamepad ID
            _currentGamepadID = -1;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from analog stick input event when the script is disabled
        InputSystem.onBeforeUpdate -= OnAnalogStickInput;

        // Disable input actions for the current Gamepad
        InputSystem.DisableDevice(UnityEngine.InputSystem.Gamepad.current);
    }

    private void EnableAnalogStickInput()
    {
        // Enable input actions for the current Gamepad
        InputSystem.EnableDevice(Gamepad.current);

        // Subscribe to analog stick input event
        InputSystem.onBeforeUpdate += OnAnalogStickInput;
    }

    private void OnAnalogStickInput()
    {
        if (Gamepad.current != null)
        {
            // Read analog stick input
            Vector2 analogInput = Gamepad.current.leftStick.ReadValue();

            // Apply deadzone to the input
            if (analogInput.magnitude < deadzone)
            {
                analogInput = Vector2.zero;
            }

            // Check if there is any significant input on the analog stick
            if (analogInput.sqrMagnitude > inputThreshold * inputThreshold)
            {
                // Calculate the rotation angle based on the analog stick input
                float rawRotationAngle = Mathf.Atan2(analogInput.y, analogInput.x) * Mathf.Rad2Deg;
                float rotationAngle = (rawRotationAngle + 360f) % 360f;

                // Adjust the rotation angle to match the desired mapping
                rotationAngle = (rotationAngle + 270f) % 360f;

                // Set the rotation of the transform with a smooth rotation over time
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    // Method to reset the rotation to the initial rotation
    public void ResetRotation()
    {
        transform.rotation = initialRotation;
    }
}
