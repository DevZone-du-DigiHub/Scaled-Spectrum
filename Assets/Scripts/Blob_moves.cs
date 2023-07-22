using UnityEngine;
using UnityEngine.InputSystem;

public class Blob_moves : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveSpeedInsideShip = 5f;
    [SerializeField] private GameObject shipObject;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isInsideShip = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        float speed = isInsideShip ? moveSpeedInsideShip : moveSpeed;
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
