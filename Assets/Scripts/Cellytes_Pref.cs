using UnityEngine;

public class Cellytes_Pref : MonoBehaviour
{
    [SerializeField] private float selfDestructTime = 5f; // Time in seconds before self-destruction
    [SerializeField] private bool spiralMovement = false; // Toggle for spiral movement
    [SerializeField] private float spiralSpeed = 2f; // Speed of spiral rotation
    [SerializeField] private int maximumQuantity = 10; // Maximum allowed quantity of projectiles
    [SerializeField] private float spawnCooldown = 2f; // Cooldown duration in seconds

    private Rigidbody2D rb;
    private float angle = 0f;
    private float cooldownTimer = 0f;
    private bool launched = false; // Flag to indicate if the projectile has been launched

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
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (spiralMovement && !launched) // Only apply spiral movement if not launched
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile collided with something other than the player
        if (!other.CompareTag("Player") && launched) // Only destroy if launched and not colliding with the player
        {
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        if (cooldownTimer <= 0f)
        {
            launched = true; // Called when you want to launch the projectile
            cooldownTimer = spawnCooldown; // Set the cooldown timer
        }
    }
}
