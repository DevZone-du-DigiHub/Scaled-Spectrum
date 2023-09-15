using UnityEngine;
using UnityEngine.InputSystem;

public class Blob_moves : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveSpeedInsideShip = 5f;
    [SerializeField] private GameObject shipObject;
    [SerializeField] private InputActionReference moveInputAction; // New field to hold the assigned input action
    private Rigidbody2D rb;
    private bool isInsideShip = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Enable the assigned input action when the script is enabled
        moveInputAction.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the assigned input action when the script is disabled
        moveInputAction.action.Disable();
    }

    private void FixedUpdate()
    {
        // Read the input value from the assigned input action
        Vector2 moveInput = moveInputAction.action.ReadValue<Vector2>();

        // Determine the speed based on whether the player is inside the ship or not
        float speed = isInsideShip ? moveSpeedInsideShip : moveSpeed;

        // Apply movement using the Rigidbody
        rb.velocity = moveInput * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ship"))
        {
            isInsideShip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ship"))
        {
            isInsideShip = false;
        }
    }

    private void Interact()
    {
        // Code for interacting

        if (!isInsideShip)
        {
            // Teleport the player to the ship's position
            transform.position = shipObject.transform.position;
        }
    }
}
