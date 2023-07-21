using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] private InputAction lookAction;
    [SerializeField] private Transform aimSightTransform;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject spriteRendererObject;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float maxDistance = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector2 lookInput;
    private Vector2 aimSightPosition;
    private Vector2 centerPosition;

    private void OnEnable()
    {
        lookAction.Enable();
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
        aimSightPosition = Vector2.zero;
    }

    private void Start()
    {
        centerPosition = aimSightTransform.localPosition;
        if(spriteRendererObject) {
            spriteRenderer = spriteRendererObject.GetComponent<SpriteRenderer>();
        }
    }

    private void LateUpdate()
    {
        aimSightPosition = lookInput * movementSpeed;
        if (aimSightPosition.magnitude > maxDistance)
        {
            aimSightPosition = aimSightPosition.normalized * maxDistance;
        }

        float rawRotationAngle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg;
        float rotationAngle = (rawRotationAngle + 360f) % 360f;
        rotationAngle = (rotationAngle + 270f) % 360f;
        aimSightTransform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        aimSightTransform.localPosition = centerPosition + aimSightPosition;

        if (playerObject != null)
        {
            transform.position = playerObject.transform.position;
        }
        if(spriteRenderer) {
            spriteRenderer.enabled = lookInput != Vector2.zero;
        }
    }

    private void OnDisable()
    {
        lookAction.Disable();
        lookAction.performed -= OnLookPerformed;
        lookAction.canceled -= OnLookCanceled;
    }
}
