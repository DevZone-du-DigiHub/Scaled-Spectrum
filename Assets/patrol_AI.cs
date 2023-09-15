using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol_AI : MonoBehaviour
{
    public float moveSpeed = 2f;   // Movement speed of the drone
    public float rotateSpeed = 180f; // Rotation speed of the drone
    public float waypointThreshold = 0.2f; // Distance threshold for reaching a waypoint

    public float minCooldown = 2f; // Minimum cooldown time after reaching a waypoint
    public float maxCooldown = 5f; // Maximum cooldown time after reaching a waypoint

    private Transform currentTarget; // Currently targeted object
    private List<string> excludedTags = new List<string> {
        "player/core", "player/visual", "player/ui", "player/equipments", "player/fx"
    };

    private float cooldownTimer = 0f; // Timer for the cooldown
    private bool isCooldown = false; // Flag to indicate if the drone is in cooldown

    void Start()
    {
        SetRandomTarget(); // Start by setting a random target
    }

    void SetRandomTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f); // Adjust the radius as needed

        List<Collider2D> validColliders = new List<Collider2D>();

        foreach (var collider in colliders)
        {
            // Check if the collider's tag is not in the exclusion list
            if (!excludedTags.Contains(collider.tag))
            {
                validColliders.Add(collider);
            }
        }

        if (validColliders.Count > 0)
        {
            Collider2D randomCollider = validColliders[Random.Range(0, validColliders.Count)];
            currentTarget = randomCollider.transform;
        }
    }

    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                SetRandomTarget();
            }
            return;
        }

        if (currentTarget == null)
        {
            SetRandomTarget();
            return;
        }

        // Exclude player from being followed
        if (currentTarget.CompareTag("player/core"))
        {
            SetRandomTarget();
            return;
        }

        // Calculate direction to the current target
        Vector2 directionToTarget = currentTarget.position - transform.position;

        // Rotate the drone towards the target
        float targetRotation = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetRotation), rotateSpeed * Time.deltaTime);

        // Move the drone forward
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // Check if the drone is close enough to the target
        if (Vector2.Distance(transform.position, currentTarget.position) < waypointThreshold)
        {
            // Set the cooldown and randomize the timer
            cooldownTimer = Random.Range(minCooldown, maxCooldown);
            isCooldown = true;
        }
    }
}
