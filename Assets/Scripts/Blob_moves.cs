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

    private void StartButton()
    {
        // Code for interacting
        Debug.Log("[Start] Pause Button");
    }

    private void SelectButton()
    {
        // Code for interacting
        Debug.Log("[Select] Select Button");
    }

    private void Shoot()
    {
        // Code for interacting
        Debug.Log("[RT] Shoot Button");
    }

    private void Contract()
    {
        // Code for interacting
        Debug.Log("[LT] Contract Button");
    }

    private void Absorb()
    {
        // Code for interacting
        Debug.Log("[RB] Absorb Button");
    }

    private void ColourSelect()
    {
        // Code for interacting
        Debug.Log("[LB] Colour Select Button");
    }

    private void Extract()
    {
        // Code for interacting
        Debug.Log("[Y] Extract Button");
    }

    private void Sprint()
    {
        // Code for interacting
        Debug.Log("[X] Sprint Button");
    }

    private void Interact()
    {
        // Code for interacting
        Debug.Log("[A] Interact Button");

        if (!isInsideShip)
        {
            // Teleport the player to the ship's position
            transform.position = shipObject.transform.position;
        }
    }

    private void Back()
    {
        // Code for interacting
        Debug.Log("[B] Back Button");
    }
}
