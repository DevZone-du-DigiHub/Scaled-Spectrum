using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorUnlocker : MonoBehaviour
{
    public string colorToUnlock = "white"; // Default color to unlock. You can change this in the inspector.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player's tag is "Player".
        {
            // Unlock the color using the ColorProgress instance.
            ColorProgress.Instance.UnlockColor(colorToUnlock);
        }
    }
}
