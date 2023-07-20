using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private int selectedProjectileIndex = 0;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private InputAction shootAction;
    [SerializeField] private Vector2 projectileDirection = Vector2.up;
    [SerializeField] private ParticleSystem muzzleFlashParticle;
    [SerializeField] private AudioSource shootingAudioSource;

    [SerializeField] private Transform spawnPoint; // Transform representing the spawn point

    private void OnEnable()
    {
        shootAction.Enable();
        shootAction.started += OnShootStarted;
    }

    private void OnDisable()
    {
        shootAction.Disable();
        shootAction.started -= OnShootStarted;
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (selectedProjectileIndex >= 0 && selectedProjectileIndex < projectilePrefabs.Length)
        {
            GameObject projectile = Instantiate(projectilePrefabs[selectedProjectileIndex], spawnPoint.position, Quaternion.identity);

            if (projectile != null)
            {
                Vector3 localProjectileDirection = transform.TransformDirection(projectileDirection.normalized);
                projectile.transform.up = localProjectileDirection;
                projectile.GetComponent<Rigidbody2D>().velocity = localProjectileDirection * projectileSpeed;

                PlayVisualFeedback();
                PlayAudioFeedback();
            }
        }
    }

    private void PlayVisualFeedback()
    {
        if (muzzleFlashParticle != null)
        {
            muzzleFlashParticle.Play();
        }
    }

    private void PlayAudioFeedback()
    {
        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }
    }

    // Methods for projectile selection
    public void SelectNextProjectile()
    {
        selectedProjectileIndex = (selectedProjectileIndex + 1) % projectilePrefabs.Length;
    }

    public void SelectPreviousProjectile()
    {
        selectedProjectileIndex = (selectedProjectileIndex - 1 + projectilePrefabs.Length) % projectilePrefabs.Length;
    }
}
