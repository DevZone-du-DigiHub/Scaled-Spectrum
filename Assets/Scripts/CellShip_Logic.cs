using UnityEngine;
using UnityEngine.InputSystem;

public class CellShip_Logic : MonoBehaviour
{
    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private Collider2D capsuleCollider;
    [SerializeField] private string highlightedAction;

    private void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                if (!capsuleCollider.OverlapPoint(transform.position))
                {
                    // Snap player to the middle of the capsule collider if outside
                    SnapToMidPoint();
                    highlightedAction = "Snap to MidPoint";
                }
                
            }
            else if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                if (!circleCollider.OverlapPoint(transform.position))
                {
                    // Snap player to the edge of the circle collider if outside
                    SnapToEdge();
                    highlightedAction = "Snap to Edge";
                }
                else
                {
                    // Teleport player outside the collider
                    TeleportOutside();
                    highlightedAction = "Teleport Outside";
                }
            }
            else if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                if (!circleCollider.OverlapPoint(transform.position))
                {
                    // Perform action specific to the A button
                    PerformActionButtonA();
                    highlightedAction = "Action A";
                }
            }
            else if (Gamepad.current.buttonWest.wasPressedThisFrame)
            {
                if (!capsuleCollider.OverlapPoint(transform.position))
                {
                    // Perform action specific to the B button
                    PerformActionButtonB();
                    highlightedAction = "Action B";
                }
            }
        }
    }

    private void SnapToMidPoint()
    {
        if (capsuleCollider != null)
        {
            // Calculate the midpoint of the capsule collider
            Vector2 midpoint = (capsuleCollider.bounds.center + capsuleCollider.bounds.min) / 2f;

            // Move the player to the midpoint
            transform.position = midpoint;
        }
    }

    private void SnapToEdge()
    {
        if (circleCollider != null)
        {
            // Calculate the position on the edge of the circle collider
            Vector2 edgePosition = circleCollider.ClosestPoint(transform.position);

            // Move the player to the edge position
            transform.position = edgePosition;
        }
    }

    private void TeleportOutside()
    {
        // Calculate a random position outside the collider
        Vector2 randomPosition = GetRandomPositionOutsideCollider();

        // Teleport the player to the random position
        transform.position = randomPosition;
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
        else if (capsuleCollider != null)
        {
            // Calculate a random position within the capsule collider
            Bounds capsuleBounds = capsuleCollider.bounds;
            float randomX = Random.Range(capsuleBounds.min.x, capsuleBounds.max.x);
            float randomY = Random.Range(capsuleBounds.min.y, capsuleBounds.max.y);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            return randomPosition;
        }

        return transform.position;
    }
}
