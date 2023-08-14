using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BlobFollow : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject

    private GameObject player; // Reference to the player GameObject

    private void Start()
    {
        // Find the player GameObject using the specified tag
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Check if the player GameObject was found
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag '" + playerTag + "' not found!");
        }
    }

    private void Update()
    {
        // Check if the player GameObject is valid
        if (player != null)
        {
            // Update the position of the current GameObject to match the player's position
            transform.position = player.transform.position;
        }
    }
}
