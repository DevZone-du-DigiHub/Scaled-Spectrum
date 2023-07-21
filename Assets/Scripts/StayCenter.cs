using UnityEngine;

public class StayCenter : MonoBehaviour
{
    [SerializeField] private GameObject cellShip;
    private SpriteRenderer spriteRenderer;

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
        }
    }
}
