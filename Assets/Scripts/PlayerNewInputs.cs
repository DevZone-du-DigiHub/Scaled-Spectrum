using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNewInputs : MonoBehaviour
{
    private Quaternion initialRotation;
    private int _currentGamepadID = -1;

    private void Start()
    {
        if(Gamepad.current != null) {
            _currentGamepadID = Gamepad.current.deviceId;
        }
        // Store the initial rotation of the object
        initialRotation = transform.rotation;
        
        if(_currentGamepadID != -1) {
            // Enable input actions
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gamepad.current);

            // Subscribe to analog stick input event
            InputSystem.onBeforeUpdate += OnAnalogStickInput;
        }
    }

    void Update()
    {
        if(Gamepad.current != null) {
            if(_currentGamepadID != Gamepad.current.deviceId) {
                _currentGamepadID = Gamepad.current.deviceId;
                InputSystem.EnableDevice(UnityEngine.InputSystem.Gamepad.current);
            }
        } else {
            if(_currentGamepadID != -1) {
                _currentGamepadID = -1;
            }
        }
    }
        private void OnDisable()
    {
        // Unsubscribe from analog stick input event
        InputSystem.onBeforeUpdate -= OnAnalogStickInput;

        // Disable input actions
        InputSystem.DisableDevice(UnityEngine.InputSystem.Gamepad.current);
    }

    private void OnAnalogStickInput()
    {
        if(Gamepad.current != null) {
            // Read analog stick input
            Vector2 analogInput = UnityEngine.InputSystem.Gamepad.current.leftStick.ReadValue();

            // Check if there is any input on the analog stick
            if (analogInput != Vector2.zero)
            {
                // Calculate the rotation angle based on the analog stick input
                float rawRotationAngle = Mathf.Atan2(analogInput.y, analogInput.x) * Mathf.Rad2Deg;
                float rotationAngle = (rawRotationAngle + 360f) % 360f;

                // Adjust the rotation angle to match the desired mapping
                rotationAngle = (rotationAngle + 270f) % 360f;

                // Set the rotation of the transform
                transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            }
        }
    }

    // Method to reset the rotation to the initial rotation
    private void ResetRotation()
    {
        transform.rotation = initialRotation;
    }
}
