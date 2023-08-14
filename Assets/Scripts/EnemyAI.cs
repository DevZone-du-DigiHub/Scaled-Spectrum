using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float detectionRadius = 5f;

    [SerializeField]
    private LayerMask obstacleLayer;

    [SerializeField]
    private float dodgeForce = 5f; // The intensity of the dodge movement

    private bool playerDetected = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the detection radius
        playerDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, LayerMask.GetMask("Player"));

        // If the player is detected, chase the player
        if (playerDetected)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Check for obstacles and dodge projectiles
        DodgeProjectiles();
    }

    // Called when a projectile touches the enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Projectile hit the enemy!");
            // Add any logic here for how the enemy reacts to being hit by a projectile
        }
    }

    // Function to dodge projectiles
    private void DodgeProjectiles()
    {
        // Find all projectiles in the scene (you might need to adjust the tag based on your project)
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        // Check each projectile to see if it's approaching the enemy
        foreach (GameObject projectile in projectiles)
        {
            Vector3 directionToProjectile = projectile.transform.position - transform.position;
            float angleToProjectile = Vector3.Angle(directionToProjectile, transform.right);

            // If the projectile is approaching the enemy, apply dodge movement
            if (angleToProjectile < 90f && directionToProjectile.magnitude < detectionRadius)
            {
                // Calculate the dodge force and direction
                Vector3 dodgeDirection = Quaternion.Euler(0f, 0f, 90f) * directionToProjectile.normalized;
                Vector2 dodgeMovement = dodgeDirection * dodgeForce;

                // Apply the dodge movement
                rb.AddForce(dodgeMovement, ForceMode2D.Impulse);
            }
        }
    }
}
