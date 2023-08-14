using UnityEngine;
using UnityEngine.InputSystem;

public class ShipTeleport : MonoBehaviour
{
    [SerializeField] private Collider2D circleCollider;

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            // Teleport player outside the collider
            TeleportOutside();
        }
    }

    private void TeleportOutside()
    {
        // Calculate a random position outside the collider
        Vector2 randomPosition = GetRandomPositionOutsideCollider();

        // Teleport the player to the random position
        transform.position = randomPosition;
    }

    private Vector2 GetRandomPositionOutsideCollider()
    {
        if (circleCollider != null)
        {
            // Calculate a random position within the circle collider
            Vector2 randomPosition = Random.insideUnitCircle.normalized * circleCollider.bounds.extents.magnitude;

            // Adjust the position to be outside the collider
            randomPosition += (Vector2)circleCollider.bounds.center;

            return randomPosition;
        }

        return transform.position;
    }
}
