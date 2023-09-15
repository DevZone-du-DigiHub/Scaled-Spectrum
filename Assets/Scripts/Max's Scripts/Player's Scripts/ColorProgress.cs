using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class ColorProgress : MonoBehaviour
{
    // Unlock flags for colors
    public bool isWhiteUnlocked = true;  // Default color always unlocked
    public bool isCyanUnlocked = false;
    public bool isMagentaUnlocked = false;
    public bool isYellowUnlocked = false;

    // Assignable UnityEvent actions for each color
    public UnityEvent onWhiteAction;
    public UnityEvent onCyanAction;
    public UnityEvent onMagentaAction;
    public UnityEvent onYellowAction;

    // Assignable GameObject references for each color unlocker
    public GameObject whiteUnlocker;
    public GameObject cyanUnlocker;
    public GameObject magentaUnlocker;
    public GameObject yellowUnlocker;

    private void Start()
    {
        // Ensure there's a Collider2D on the player
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("No Collider2D found on the player. Please attach a Collider2D for collision detection to work.");
        }

        // Ensure there's a Rigidbody2D on the player
        if (GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogWarning("No Rigidbody2D found on the player. Please attach a Rigidbody2D for collision detection to work. It can be set to kinematic if you don't want physics-based movement.");
        }

        // Ensure the unlockers have their colliders set as triggers
        CheckTriggerSetting(whiteUnlocker, "White Unlocker");
        CheckTriggerSetting(cyanUnlocker, "Cyan Unlocker");
        CheckTriggerSetting(magentaUnlocker, "Magenta Unlocker");
        CheckTriggerSetting(yellowUnlocker, "Yellow Unlocker");
    }

    private void CheckTriggerSetting(GameObject unlocker, string name)
    {
        if (unlocker != null && unlocker.GetComponent<Collider2D>() != null && !unlocker.GetComponent<Collider2D>().isTrigger)
        {
            Debug.LogWarning(name + " does not have its Collider2D set as a Trigger. Please set it as a trigger in the Unity Editor.");
        }
    }

    // Singleton Pattern for easy access
    public static ColorProgress Instance;

    public void UnlockColor(string colorName)
    {
        switch (colorName)
        {
            case "White":
                isWhiteUnlocked = true;
                onWhiteAction.Invoke();
                break;
            case "Cyan":
                isCyanUnlocked = true;
                onCyanAction.Invoke();
                break;
            case "Magenta":
                isMagentaUnlocked = true;
                onMagentaAction.Invoke();
                break;
            case "Yellow":
                isYellowUnlocked = true;
                onYellowAction.Invoke();
                break;
        }
    }

    public bool IsColorUnlocked(string colorName)
    {
        switch (colorName)
        {
            case "White":
                return isWhiteUnlocked;
            case "Cyan":
                return isCyanUnlocked;
            case "Magenta":
                return isMagentaUnlocked;
            case "Yellow":
                return isYellowUnlocked;
            default:
                return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == whiteUnlocker)
        {
            UnlockColor("White");
        }
        else if (other.gameObject == cyanUnlocker)
        {
            UnlockColor("Cyan");
        }
        else if (other.gameObject == magentaUnlocker)
        {
            UnlockColor("Magenta");
        }
        else if (other.gameObject == yellowUnlocker)
        {
            UnlockColor("Yellow");
        }
    }

    // Methods to be called by PlayMaker
    public void SetWhiteColor()
    {
        if (isWhiteUnlocked)
        {
            onWhiteAction.Invoke();
        }
    }

    public void SetCyanColor()
    {
        if (isCyanUnlocked)
        {
            onCyanAction.Invoke();
        }
    }

    public void SetMagentaColor()
    {
        if (isMagentaUnlocked)
        {
            onMagentaAction.Invoke();
        }
    }

    public void SetYellowColor()
    {
        if (isYellowUnlocked)
        {
            onYellowAction.Invoke();
        }
    }
}
