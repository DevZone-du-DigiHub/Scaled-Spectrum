using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Don't forget to include the UnityEvents namespace

public class projectil_stateFX : MonoBehaviour
{
    [SerializeField] private bool enableFlicker = false;
    [SerializeField] private float flickerInterval = 0.1f;
    [SerializeField] private bool rotateClockwise = true;
    [SerializeField] private float rotationSpeed = 45f;

    private SpriteRenderer spriteRenderer;
    private bool isFlickering = false;

    // UnityEvent actions
    [SerializeField] private UnityEvent onFlickerEnabled;
    [SerializeField] private UnityEvent onFlickerDisabled;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Rotate();
    }

    public void StartFlickeringExternally()
    {
        if (enableFlicker && !isFlickering)
        {
            StartFlickering();
        }
    }

    public void StopFlickeringExternally()
    {
        if (isFlickering)
        {
            StopFlickering();
        }
    }

    public void EnableRotation()
    {
        enabled = true; // Enable the Update loop to resume rotation
        onFlickerEnabled.Invoke(); // Invoke the onFlickerEnabled UnityEvent
    }

    public void DisableRotation()
    {
        enabled = false; // Disable the Update loop to pause rotation
        onFlickerDisabled.Invoke(); // Invoke the onFlickerDisabled UnityEvent
    }

    private void StartFlickering()
    {
        isFlickering = true;
        StartCoroutine(FlickerCoroutine());
    }

    private void StopFlickering()
    {
        isFlickering = false;
        spriteRenderer.enabled = true; // Ensure the sprite renderer is enabled when flickering stops
    }

    private IEnumerator FlickerCoroutine()
    {
        while (isFlickering)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flickerInterval);
        }

        spriteRenderer.enabled = true; // Ensure the sprite renderer is enabled when flickering stops
    }

    private void Rotate()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        if (!rotateClockwise)
        {
            rotationAmount *= -1f;
        }

        transform.Rotate(Vector3.forward, rotationAmount);
    }
}
