using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_AutoMove : MonoBehaviour
{
    public float moveDistance = 100f; // How far up and down the UI element should move
    public float moveDuration = 1f; // How long it takes to complete one move up or down

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private float moveTimer;
    private bool movingUp = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        moveTimer = moveDuration; // Set the timer to the move duration to start the movement immediately
    }

    void Update()
    {
        // Update the move timer
        moveTimer += movingUp ? Time.deltaTime : -Time.deltaTime;

        // Calculate the new position based on the move timer
        float newYPosition = originalPosition.y + (moveDistance * (moveTimer / moveDuration));
        rectTransform.anchoredPosition = new Vector2(originalPosition.x, newYPosition);

        // Switch direction if the timer exceeds the duration or goes below 0
        if (moveTimer > moveDuration || moveTimer < 0)
        {
            movingUp = !movingUp;
        }
    }
}
