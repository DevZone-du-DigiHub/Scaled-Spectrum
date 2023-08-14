using UnityEngine;
using UnityEngine.InputSystem;

public class ShipActionButton : MonoBehaviour
{
    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private Collider2D capsuleCollider;
    [SerializeField] private GameObject imageToActivate;
    [SerializeField] private float imageActivationDistance = 1.5f;

    private void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                if (!circleCollider.OverlapPoint(transform.position))
                {
                    // Perform action specific to the A button
                    PerformActionButtonA();
                }
            }
            else if (Gamepad.current.buttonWest.wasPressedThisFrame)
            {
                if (!capsuleCollider.OverlapPoint(transform.position))
                {
                    // Perform action specific to the B button only if inside the ship and within activation distance
                    if (Vector2.Distance(transform.position, capsuleCollider.bounds.center) <= imageActivationDistance)
                    {
                        PerformActionButtonB();
                    }
                }
            }
        }
    }

    private void PerformActionButtonA()
    {
        // Code to perform action specific to the A button
        Debug.Log("Action A");
    }

    private void PerformActionButtonB()
    {
        // Code to perform action specific to the B button
        Debug.Log("Action B");
    }
}
