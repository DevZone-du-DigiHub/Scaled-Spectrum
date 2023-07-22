using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;


public class PressedButtonLogs : MonoBehaviour
{
    private int _currentGamepadID = -1;
    private string _currentGamepadName;
        
    void Start()
    {
        if(Gamepad.current != null) {
            _currentGamepadID = Gamepad.current.deviceId;
            _currentGamepadName = Gamepad.current.displayName;
            Debug.Log(Gamepad.current.displayName + " is now connected.");
        }
    }

    void Update()
    {
        if(Gamepad.current != null) {
            if(_currentGamepadID != Gamepad.current.deviceId && _currentGamepadID != -1) {
                Debug.Log(_currentGamepadName + " disconnected.");
                Debug.Log(Gamepad.current.displayName + " is now connected.");
                _currentGamepadID = Gamepad.current.deviceId;
                _currentGamepadName = Gamepad.current.displayName;
            }
            if(_currentGamepadID == -1) {
                Debug.Log(Gamepad.current.displayName + " is now connected.");
                _currentGamepadID = Gamepad.current.deviceId;
                _currentGamepadName = Gamepad.current.displayName;
            }
                ControllerLog();
        } else {
            if(_currentGamepadID != -1) {
                Debug.Log(_currentGamepadName + " disconnected.");
                _currentGamepadID = -1;
                _currentGamepadName = null;
            }
        }
    }

    void ControllerLog() 
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame){
            Debug.Log("[A] Interact Button");
        }
        if (Gamepad.current.buttonEast.wasPressedThisFrame){
            Debug.Log("[B] Unknown");
        }
        if (Gamepad.current.buttonNorth.wasPressedThisFrame){
            Debug.Log("[Y] Extract Button");
        }
        if (Gamepad.current.buttonWest.wasPressedThisFrame){
            Debug.Log("[X] Sprint Button");
        }
        if (Gamepad.current.leftShoulder.wasPressedThisFrame){
            Debug.Log("[LB] Colour Select Button");
        }
        if (Gamepad.current.leftTrigger.wasPressedThisFrame){
            Debug.Log("[LT] Contract Button");
        }
        if (Gamepad.current.rightShoulder.wasPressedThisFrame){
            Debug.Log("[RB] Absorb Button");
        }
        if (Gamepad.current.rightTrigger.wasPressedThisFrame){
            Debug.Log("[RT] Shoot Button");
        }
        if (Gamepad.current.startButton.wasPressedThisFrame){
            Debug.Log("[Start] Pause Button");
        }
        if (Gamepad.current.selectButton.wasPressedThisFrame){
            Debug.Log("[Select] Select Button");
        }
        if (Gamepad.current.leftStickButton.wasPressedThisFrame){
            Debug.Log("[L3] Unknown Button");
        }
        if (Gamepad.current.rightStickButton.wasPressedThisFrame){
            Debug.Log("[R3] Unknown Button");
        }
        if (Gamepad.current.dpad.right.wasPressedThisFrame){
            Debug.Log("[D-Right] Unknown Button");
        }
        if (Gamepad.current.dpad.left.wasPressedThisFrame){
            Debug.Log("[D-Left] Unknown Button");
        }
        if (Gamepad.current.dpad.up.wasPressedThisFrame){
            Debug.Log("[D-Up] Unknown Button");
        }
        if (Gamepad.current.dpad.down.wasPressedThisFrame){
            Debug.Log("[D-Down] Unknown Button");
        }
        if (Gamepad.current.leftStick.right.wasPressedThisFrame){
            Debug.Log("[LS Right] Unknown Button");
        }
        if (Gamepad.current.leftStick.left.wasPressedThisFrame){
            Debug.Log("[LS Left] Unknown Button");
        }
        if (Gamepad.current.leftStick.up.wasPressedThisFrame){
            Debug.Log("[LS Up] Unknown Button");
        }
        if (Gamepad.current.leftStick.down.wasPressedThisFrame){
            Debug.Log("[LS Down] Unknown Button");
        }
        if (Gamepad.current.rightStick.right.wasPressedThisFrame){
            Debug.Log("[RS Right] Unknown Button");
        }
        if (Gamepad.current.rightStick.left.wasPressedThisFrame){
            Debug.Log("[RS Left] Unknown Button");
        }
        if (Gamepad.current.rightStick.up.wasPressedThisFrame){
            Debug.Log("[RS Up] Unknown Button");
        }
        if (Gamepad.current.rightStick.down.wasPressedThisFrame){
            Debug.Log("[RS Down] Unknown Button");
        }
    }
}
