using UnityEngine;

public class ImageActivation : MonoBehaviour
{
    [SerializeField] private Collider2D capsuleCollider;
    [SerializeField] private GameObject imageToActivate;
    [SerializeField] private float imageActivationDistance = 1.5f;

    private void Update()
    {
        if (imageToActivate != null)
        {
            // Check if the player is inside the ship to activate the image
            imageToActivate.SetActive(Vector2.Distance(transform.position, capsuleCollider.bounds.center) <= imageActivationDistance);
        }
    }
}
