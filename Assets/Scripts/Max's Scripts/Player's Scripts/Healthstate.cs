using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Healthstate : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int baseMaxHealth = 4;
    [SerializeField] private int cappedMaxHealth;
    [SerializeField] private int currentHealth;

    [Header("UI Elements")]
    [SerializeField] private Image[] healthImages;
    [Header("Cooldown Settings")]
    [SerializeField] private float getHurtCooldown = 1.0f;
    [SerializeField] private float getHealCooldown = 0.5f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip getHurtSound;
    [SerializeField] private AudioClip healingSound;
    [SerializeField] private AudioClip noLifeSound;

    [Header("Collision Tags/ Add & increase")]
    [SerializeField] private string hazardTag = "hazard";
    [SerializeField] private string healOneTag = "heal/one";
    [SerializeField] private string healDoubleTag = "heal/double";
    [SerializeField] private string healFullTag = "heal/full";
    [SerializeField] private string increaseMaxHealthTag = "increase/maxhealth";

    [Header("Collision Tags/ decrease & death")]
    [SerializeField] private string hurtOneTag = "hurt/one";
    [SerializeField] private string hurtDoubleTag = "hurt/double";
    [SerializeField] private string hurtTripleTag = "hurt/triple";
    [SerializeField] private string hurtFullTag = "hurt/full";

    [Header("Events")]
    [SerializeField] private UnityEvent onPlayerDeathEvent;

    private bool canTakeDamage = true;

    private void Start()
    {
        InitializeAudioSource();
        cappedMaxHealth = baseMaxHealth;
        currentHealth = cappedMaxHealth;
        UpdateHealthBar();
    }

    private void InitializeAudioSource()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage && other.CompareTag(hazardTag))
        {
            DecreaseHealth();
            StartCoroutine(GetHurtCooldown());
        }
        else if (other.CompareTag(healOneTag) || other.CompareTag(healDoubleTag) || other.CompareTag(healFullTag))
        {
            IncreaseHealth(other.CompareTag(healDoubleTag) ? 2 : cappedMaxHealth - currentHealth);
            StartCoroutine(GetHealCooldown());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(increaseMaxHealthTag))
        {
            cappedMaxHealth++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(hurtOneTag) || other.CompareTag(hurtDoubleTag) || other.CompareTag(hurtTripleTag) || other.CompareTag(hurtFullTag))
        {
            int hurtAmount = 0;
            if (other.CompareTag(hurtOneTag)) hurtAmount = 1;
            else if (other.CompareTag(hurtDoubleTag)) hurtAmount = 2;
            else if (other.CompareTag(hurtTripleTag)) hurtAmount = 3;
            else if (other.CompareTag(hurtFullTag)) hurtAmount = currentHealth;

            DecreaseHealth(hurtAmount);
        }
    }

    private void DecreaseHealth()
    {
        DecreaseHealth(1);
    }

    private void DecreaseHealth(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died");
            PlaySound(noLifeSound);
            if (onPlayerDeathEvent != null)
            {
                onPlayerDeathEvent.Invoke();
            }
        }
        else
        {
            PlaySound(getHurtSound);
        }

        UpdateHealthBar();
    }

    private void IncreaseHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, cappedMaxHealth);
        PlaySound(healingSound);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            healthImages[i].enabled = i < currentHealth;
        }
    }

    private IEnumerator GetHurtCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(getHurtCooldown);
        canTakeDamage = true;
    }

    private IEnumerator GetHealCooldown()
    {
        yield return new WaitForSeconds(getHealCooldown);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
