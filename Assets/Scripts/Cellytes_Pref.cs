using UnityEngine;

public class Cellytes_Pref : MonoBehaviour
{
    [SerializeField] private float selfDestructTime = 5f; // Time in seconds before self-destruction
    [SerializeField] private bool spiralMovement = false; // Toggle for spiral movement
    [SerializeField] private float spiralSpeed = 2f; // Speed of spiral rotation
    [SerializeField] private int maximumQuantity = 10; // Maximum allowed quantity of projectiles

    private Rigidbody2D rb;
    private float angle = 0f;

    private static int currentQuantity = 0; // Current quantity of projectiles

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("SelfDestruct", selfDestructTime);
        IncrementCurrentQuantity();
    }

    private void Update()
    {
        if (spiralMovement)
        {
            angle += spiralSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void OnDestroy()
    {
        DecrementCurrentQuantity();
    }

    private void IncrementCurrentQuantity()
    {
        currentQuantity++;
        if (currentQuantity > maximumQuantity)
        {
            // Reached the maximum quantity, self-destruct immediately
            Destroy(gameObject);
        }
    }

    private void DecrementCurrentQuantity()
    {
        currentQuantity--;
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
