using UnityEngine;
using UnityEngine.InputSystem;


public class SnapIndicator : MonoBehaviour
{
    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private Collider2D capsuleCollider;
    [SerializeField] private GameObject snapIndicator;
    [SerializeField] private float snapDistance = 2f;

    private void Update()
    {
        if (snapIndicator != null && Gamepad.current != null)
        {
            // Show or hide the snap indicator based on the player's distance from the ship and not inside the snap indicator
            snapIndicator.SetActive(!circleCollider.OverlapPoint(transform.position) &&
                                   Vector2.Distance(transform.position, capsuleCollider.bounds.center) <= snapDistance);
        }
        else if (snapIndicator != null)
        {
            snapIndicator.SetActive(false);
        }
    }
}
