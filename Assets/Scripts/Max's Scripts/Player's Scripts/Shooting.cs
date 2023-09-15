using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject whiteProjectile;
    [SerializeField] private GameObject cyanProjectile;
    [SerializeField] private GameObject magentaProjectile;
    [SerializeField] private GameObject yellowProjectile;

    private GameObject currentProjectile;

    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private InputAction shootAction;
    [SerializeField] private Vector2 projectileDirection = Vector2.up;
    [SerializeField] private ParticleSystem muzzleFlashParticle;
    [SerializeField] private AudioSource shootingAudioSource;

    [SerializeField] private Transform spawnPoint; // Transform representing the spawn point

    private void Start()
    {
        SetWhiteProjectile(); // Default projectile
    }

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
        ShootProjectile();
    }

    public void SetWhiteProjectile()
    {
        currentProjectile = whiteProjectile;
    }

    public void SetCyanProjectile()
    {
        currentProjectile = cyanProjectile;
    }

    public void SetMagentaProjectile()
    {
        currentProjectile = magentaProjectile;
    }

    public void SetYellowProjectile()
    {
        currentProjectile = yellowProjectile;
    }

    private void ShootProjectile()
    {
        if (currentProjectile != null)
        {
            GameObject projectile = Instantiate(currentProjectile, spawnPoint.position, Quaternion.identity);

            Vector3 localProjectileDirection = transform.TransformDirection(projectileDirection.normalized);
            projectile.transform.up = localProjectileDirection;
            projectile.GetComponent<Rigidbody2D>().velocity = localProjectileDirection * projectileSpeed;

            PlayVisualFeedback();
            PlayAudioFeedback();
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
}
