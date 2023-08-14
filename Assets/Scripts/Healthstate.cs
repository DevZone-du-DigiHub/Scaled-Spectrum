using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Healthstate : MonoBehaviour
{
    public int health = 3;
    public Image health3Image;
    public Image health2Image;
    public Image health1Image;
    public float waitTime = 1.0f;
    private bool canTakeDamage = true;

    // Audio settings
    private AudioSource audioSource;
    public AudioClip hurtSound;   // Sound to play when hurt
    public AudioClip lifeSound;   // Sound to play when gaining life
    public AudioClip noLifeSound; // Sound to play when there's no life left

    private void Start()
    {
        // Try to get the AudioSource component attached to this game object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // If none found, create one
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage)
        {
            if (other.CompareTag("enemy") || other.CompareTag("hazard"))
            {
                DecreaseHealth();
                StartCoroutine(DamageCooldown());
            }
            else if (other.CompareTag("life"))
            {
                IncreaseHealth();
            }
        }
    }

    private void DecreaseHealth()
    {
        health--;

        if (health <= 0)
        {
            Debug.Log("Player has died");
            PlaySound(noLifeSound); // Play no life sound
        }
        else
        {
            PlaySound(hurtSound); // Play hurt sound
        }

        UpdateHealthBar();
    }

    private void IncreaseHealth()
    {
        if (health > 0)
        {
            health = Mathf.Min(3, health + 1);
            PlaySound(lifeSound); // Play life sound
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        health3Image.enabled = false;
        health2Image.enabled = false;
        health1Image.enabled = false;

        switch (health)
        {
            case 3:
                health3Image.enabled = true;
                break;
            case 2:
                health2Image.enabled = true;
                break;
            case 1:
                health1Image.enabled = true;
                break;
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(waitTime);
        canTakeDamage = true;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
