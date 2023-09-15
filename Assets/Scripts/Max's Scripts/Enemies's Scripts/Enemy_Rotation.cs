using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 180f; // Editable field for rotation speed
    [SerializeField] private string playerTag = "Player"; // Editable field for player tag

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        if (player != null)
        {
            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float currentAngle = transform.rotation.eulerAngles.z;

        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }
}
