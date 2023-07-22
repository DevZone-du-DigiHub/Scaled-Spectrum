using UnityEngine;

public class StayCenter : MonoBehaviour
{
    [SerializeField] private GameObject cellShip;
    [SerializeField] private Canvas canvasToActivate;
    private SpriteRenderer spriteRenderer;

    private bool isPlayerInside = false;

    private void Start()
    {
        spriteRenderer = cellShip.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (spriteRenderer != null)
        {
            // Calculate the center of the sprite
            Vector3 center = spriteRenderer.bounds.center;

            // Move the object to the center of the sprite
            transform.position = center;

            if (isPlayerInside)
            {
                // Activate the canvasToActivate when the player is inside the cellShip
                canvasToActivate.enabled = true;
            }
            else
            {
                // Deactivate the canvasToActivate when the player is outside the cellShip
                canvasToActivate.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Assuming the player object has a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Set the flag to true when the player is inside the cellShip
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Assuming the player object has a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Set the flag to false when the player is outside the cellShip
            isPlayerInside = false;
        }
    }
}
