using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TriggerMode
{
    OnTriggerEnter,
    OnTriggerExit,
    OnCollisionEnter,
    OnCollisionExit
}

public class PlayerInputLogic : MonoBehaviour
{
    public PlayerInput playerInput;

    public void SetPlayerInput(PlayerInput playerInputComponent)
    {
        playerInput = playerInputComponent;
    }

    public bool IsPlayerInputTriggered(string playerInputAction)
    {
        if (playerInput == null)
        {
            Debug.LogWarning("Player input is not set. Please set the player input component.");
            return false;
        }

        InputAction action = playerInput.currentActionMap.FindAction(playerInputAction, true);
        return action != null && action.triggered;
    }
}

[System.Serializable]
public class FeatureAction
{
    public TriggerMode triggerMode;
    public FeatureElement featureElement;
}

[System.Serializable]
public class FeatureElement
{
    public enum ElementType
    {
        GameObject,
        Component,
        Script,
        Tag,
        Layer,
        PlayerInput
    }

    public ElementType elementType;
    public Object elementObject; // Change the type to UnityEngine.Object
    public string selectedTag;
    public int selectedLayer;
}

#if UNITY_EDITOR
[CustomEditor(typeof(FeatureElement))]
public class FeatureElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        FeatureElement.ElementType elementType = (FeatureElement.ElementType)serializedObject.FindProperty("elementType").enumValueIndex;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("elementType"));

        if (elementType == FeatureElement.ElementType.Tag)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedTag"));
        }
        else if (elementType == FeatureElement.ElementType.Layer)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedLayer"));
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("elementObject"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

public class FeatureToggler : MonoBehaviour
{
    [SerializeField]
    private FeatureAction action;

    public GameObject target;

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (CheckTrigger(action.triggerMode, action.featureElement))
        {
            EnableFeature(action.featureElement);
        }
        else
        {
            DisableFeature(action.featureElement);
        }
    }

    private bool CheckTrigger(TriggerMode triggerMode, FeatureElement element)
    {
        switch (triggerMode)
        {
            case TriggerMode.OnTriggerEnter:
                return IsTriggerEnter(element);
            case TriggerMode.OnTriggerExit:
                return IsTriggerExit(element);
            case TriggerMode.OnCollisionEnter:
                return IsCollisionEnter(element);
            case TriggerMode.OnCollisionExit:
                return IsCollisionExit(element);
            // Add more cases for additional trigger modes if needed.
            default:
                return false;
        }
    }

    private bool IsTriggerEnter(FeatureElement element)
    {
        if (element.elementType == FeatureElement.ElementType.Script)
        {
            MonoBehaviour script = element.elementObject as MonoBehaviour;
            return script != null && script == target.GetComponent<MonoBehaviour>();
        }
        return false;
    }

    private bool IsTriggerExit(FeatureElement element)
    {
        if (element.elementType == FeatureElement.ElementType.Script)
        {
            MonoBehaviour script = element.elementObject as MonoBehaviour;
            return script != null && script == target.GetComponent<MonoBehaviour>();
        }
        return false;
    }

    private bool IsCollisionEnter(FeatureElement element)
    {
        if (element.elementType == FeatureElement.ElementType.Script)
        {
            MonoBehaviour script = element.elementObject as MonoBehaviour;
            return script != null && script == target.GetComponent<MonoBehaviour>();
        }
        return false;
    }

    private bool IsCollisionExit(FeatureElement element)
    {
        if (element.elementType == FeatureElement.ElementType.Script)
        {
            MonoBehaviour script = element.elementObject as MonoBehaviour;
            return script != null && script == target.GetComponent<MonoBehaviour>();
        }
        return false;
    }

    private void EnableFeature(FeatureElement elementToEnable)
    {
        switch (elementToEnable.elementType)
        {
            case FeatureElement.ElementType.Script:
                MonoBehaviour script = elementToEnable.elementObject as MonoBehaviour;
                if (script != null)
                {
                    script.enabled = true;
                }
                break;
            // Add cases for other element types if needed...
            default:
                Debug.LogWarning("Unsupported element type for enabling feature.");
                break;
        }
    }

    private void DisableFeature(FeatureElement elementToDisable)
    {
        switch (elementToDisable.elementType)
        {
            case FeatureElement.ElementType.Script:
                MonoBehaviour script = elementToDisable.elementObject as MonoBehaviour;
                if (script != null)
                {
                    script.enabled = false;
                }
                break;
            // Add cases for other element types if needed...
            default:
                Debug.LogWarning("Unsupported element type for disabling feature.");
                break;
        }
    }
}
