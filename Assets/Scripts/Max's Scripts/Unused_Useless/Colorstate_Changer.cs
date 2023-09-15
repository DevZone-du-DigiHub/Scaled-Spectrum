using UnityEngine;

public class Colorstate_Changer : MonoBehaviour
{
    private string lastTag; // Keep track of the last tag

    [Header("Sprite Renderer Reference")]
    public SpriteRenderer targetRenderer; // Assignable field for SpriteRenderer

    [Header("Materials for each tag")]
    public Material whiteMaterial;
    public Material cyanMaterial;
    public Material magentaMaterial;
    public Material yellowMaterial;

    [Header("Unlockable Colors")]
    [HideInInspector] public bool isWhiteUnlocked = true; // Always unlocked as default
    [HideInInspector] public bool isCyanUnlocked = false;
    [HideInInspector] public bool isMagentaUnlocked = false;
    [HideInInspector] public bool isYellowUnlocked = false;

    private void Start()
    {
        if (!targetRenderer)
        {
            Debug.LogError("SpriteRenderer reference not set!");
            return;
        }

        lastTag = gameObject.tag; // Initialize with the current tag.
        SetMaterialBasedOnTag(lastTag); // Ensure initial state material is set
    }

    private void Update()
    {
        if (gameObject.tag != lastTag)
        {
            SetMaterialBasedOnTag(gameObject.tag);
            lastTag = gameObject.tag;
        }
    }

    private void SetMaterialBasedOnTag(string tag)
    {
        switch (tag)
        {
            case "White":
                if (isWhiteUnlocked) targetRenderer.material = whiteMaterial;
                break;
            case "Cyan":
                if (isCyanUnlocked) targetRenderer.material = cyanMaterial;
                break;
            case "Magenta":
                if (isMagentaUnlocked) targetRenderer.material = magentaMaterial;
                break;
            case "Yellow":
                if (isYellowUnlocked) targetRenderer.material = yellowMaterial;
                break;
            default:
                Debug.LogWarning("Tag not recognized: " + tag);
                break;
        }
    }

    // Public method to unlock colors from outside this script
    public void UnlockColor(string colorName)
    {
        switch (colorName)
        {
            case "White":
                isWhiteUnlocked = true;
                break;
            case "Cyan":
                isCyanUnlocked = true;
                break;
            case "Magenta":
                isMagentaUnlocked = true;
                break;
            case "Yellow":
                isYellowUnlocked = true;
                break;
            default:
                Debug.LogWarning("Color not recognized: " + colorName);
                break;
        }
    }
}
