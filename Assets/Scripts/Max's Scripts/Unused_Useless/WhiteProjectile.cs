using UnityEngine;

public class WhiteProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Color_Key colorKey = other.GetComponent<Color_Key>();

            if (colorKey != null)
            {
                // Call the method to indicate a collision with a white projectile
                
            }
        }
    }
}
