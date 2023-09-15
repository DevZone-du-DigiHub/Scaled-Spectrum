using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Chase : MonoBehaviour
{
    public float chaseSpeed = 5f;

    private Rigidbody2D rb;
    private GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        Vector2 chaseDirection = directionToPlayer.normalized;

        // Apply force to move the enemy
        rb.velocity = chaseDirection * chaseSpeed;
    }
}
