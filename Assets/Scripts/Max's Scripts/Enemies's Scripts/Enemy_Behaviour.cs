using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy_Behaviour : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float awarenessRadius = 10f;
    [SerializeField] private float chaseRadius = 8f;

    [Header("Detection Settings")]
    public float chargeDelay = 2f;
    public float chargeDuration = 3f;

    [Header("Movement Speeds")]
    public float awarenessSpeed = 2f; // Speed when player is in awareness radius
    public float chaseSpeed = 5f;     // Speed when player is in chase radius

    [Header("Raycasting Settings")]
    public LayerMask obstacleLayer;

    [Header("Tag Names")]
    [SerializeField] private string playerTag = "Player"; // Editable in the Inspector

    [Header("Events")]
    public UnityEvent onChargingStarted;
    public UnityEvent onChargingStopped;
    public UnityEvent onPlayerEnteredAwareness;
    public UnityEvent onPlayerEnteredChase;

    private bool isCharging;
    private GameObject player;
    private float detectionStartTime;
    private bool playerInsideAwareness;
    private bool playerInsideChase;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    private void Update()
    {
        if (player != null)
        {
            CheckDetection();

            if (isCharging)
            {
                ChargePlayer();
            }
        }
    }

    private void CheckDetection()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= awarenessRadius)
        {
            if (!playerInsideAwareness)
            {
                playerInsideAwareness = true;
                onPlayerEnteredAwareness.Invoke();
            }

            if (!isCharging && Time.time - detectionStartTime >= chargeDelay)
            {
                detectionStartTime = Time.time;

                bool obstacleDetected = CheckObstacleBetweenEnemyAndPlayer(distanceToPlayer);
                SetChargingState(!obstacleDetected);
            }
        }
        else
        {
            if (playerInsideAwareness)
            {
                playerInsideAwareness = false;
                SetChargingState(false);
            }

            detectionStartTime = Time.time;
        }

        if (distanceToPlayer <= chaseRadius)
        {
            playerInsideChase = true;
            onPlayerEnteredChase.Invoke();
        }
        else
        {
            playerInsideChase = false;
        }
    }

    private bool CheckObstacleBetweenEnemyAndPlayer(float distance)
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distance, obstacleLayer);

        return hit.collider != null && hit.collider.CompareTag(playerTag);
    }

    private void ChargePlayer()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, obstacleLayer);

        if (hit.collider == null)
        {
            float currentSpeed = playerInsideChase ? chaseSpeed : awarenessSpeed;
            Vector2 chargeDirection = directionToPlayer.normalized;
            transform.position += (Vector3)(chargeDirection * currentSpeed * Time.deltaTime);

            if (Time.time - detectionStartTime >= chargeDuration)
            {
                SetChargingState(false);
            }
        }
        else
        {
            SetChargingState(false);
        }
    }

    private void SetChargingState(bool state)
    {
        if (isCharging != state)
        {
            isCharging = state;
            if (isCharging)
            {
                onChargingStarted.Invoke();
            }
            else
            {
                onChargingStopped.Invoke();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Implement the logic for the enemy taking damage here
        // You can reduce the enemy's health, play hit animation, etc.
    }

    private void OnDrawGizmosSelected()
    {
        // Draw awareness radius
        Gizmos.color = new Color(1f, 0.92f, 0.016f, 0.5f); // Yellow with alpha
        Gizmos.DrawWireSphere(transform.position, awarenessRadius);

        // Draw chase radius
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f); // Red with alpha
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}