using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Color_Key : MonoBehaviour
{
    [Header("Collision Tag")]
    [SerializeField] private string collisionTag = "YourCollisionTag"; // Specify the tag in the Inspector

    [Header("Health")]
    [SerializeField] private int startingHealth = 100; // Specify the starting health of the enemy

    [Header("Damage Over Time")]
    [SerializeField] private float damageDuration = 3f; // Specify the duration of damage in seconds
    [SerializeField] private int damageAmountPerSecond = 10; // Specify the damage amount per second

    [Header("Flicker Settings")]
    [SerializeField] private float flickerDuration = 0.2f; // Specify the duration of each flicker
    [SerializeField] private float flickerCooldown = 1f; // Specify the cooldown before restoring default state
    [SerializeField] private int flickerCycles = 5; // Specify the number of on and off flicker cycles
    [SerializeField] private SpriteRenderer flickerSpriteRenderer; // Reference to the SpriteRenderer for flickering

    [Header("Cooldown Events")]
    public UnityEvent onCooldownStart; // UnityEvent invoked when the cooldown starts
    public UnityEvent onCooldownEnd; // UnityEvent invoked when the cooldown ends

    private float damageTimer;
    private int currentHealth; // Current health of the enemy

    private bool isOnCooldown = false;

    private void Start()
    {
        // Initialize any necessary components or settings
        damageTimer = damageDuration;

        // Set the current health to the starting health
        currentHealth = startingHealth;

        // Enable the sprite renderer by default
        flickerSpriteRenderer.enabled = true;
    }

    private void Update()
    {
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0f)
            {
                // Reset the damage timer
                damageTimer = damageDuration;

                // Enable the sprite renderer when not flickering
                flickerSpriteRenderer.enabled = true;
            }
        }

        Debug.Log("Enemy Health: " + currentHealth); // Display health in the console
    }

    // Called when the enemy's collider is hit by a projectile
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(collisionTag) && !isOnCooldown)
        {
            currentHealth -= damageAmountPerSecond;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            damageTimer = damageDuration;

            Debug.Log("Enemy hit by specified tag. Health: " + currentHealth);

            // Start the flicker coroutine when hit
            StartCoroutine(FlickerCoroutine());
        }
    }

    private IEnumerator FlickerCoroutine()
    {
        isOnCooldown = true;
        onCooldownStart.Invoke(); // Invoke the cooldown start event

        flickerSpriteRenderer.enabled = false;

        for (int cycle = 0; cycle < flickerCycles; cycle++)
        {
            // Toggle the sprite renderer state
            flickerSpriteRenderer.enabled = !flickerSpriteRenderer.enabled;

            // Wait for the flicker duration
            yield return new WaitForSeconds(flickerDuration);
        }

        // Ensure the sprite renderer is enabled after flickering
        flickerSpriteRenderer.enabled = true;

        // Start the cooldown coroutine
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        // Wait for the cooldown duration
        yield return new WaitForSeconds(flickerCooldown);

        // Enable the sprite renderer after cooldown
        flickerSpriteRenderer.enabled = true;

        isOnCooldown = false;
        onCooldownEnd.Invoke(); // Invoke the cooldown end event
    }
}
