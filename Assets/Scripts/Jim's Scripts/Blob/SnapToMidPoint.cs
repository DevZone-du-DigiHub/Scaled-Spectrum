using UnityEngine;
using UnityEngine.InputSystem;

public class SnapToMidPoint : MonoBehaviour
{
    [SerializeField] private Collider2D capsuleCollider;
    [SerializeField] private float snapDistance = 2f;

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (!capsuleCollider.OverlapPoint(transform.position))
            {
                // Snap player to the middle of the capsule collider if outside and within snapDistance
                if (Vector2.Distance(transform.position, capsuleCollider.bounds.center) <= snapDistance)
                {
                    SnapPlayerToMidPoint();
                }
            }
        }
    }

    private void SnapPlayerToMidPoint()
    {
        if (capsuleCollider != null)
        {
            // Calculate the midpoint of the capsule collider
            Vector2 midpoint = (capsuleCollider.bounds.center + capsuleCollider.bounds.min) / 2f;

            // Move the player to the midpoint
            transform.position = midpoint;
        }
    }
}
