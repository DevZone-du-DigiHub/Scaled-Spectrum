using UnityEngine;
using UnityEngine.Events;

public class ComplexProjectileSystem : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    [Header("Collision Settings")]
    [SerializeField] private bool destroyOnCollision = true;
    [SerializeField] private string[] collisionTags;

    [Header("Events")]
    [SerializeField] private UnityEvent onProjectileHitEvent;
    [SerializeField] private UnityEvent onProjectileExpiredEvent;

    private bool launched = false;
    private float launchTime;

    private void Start()
    {
        Launch();
    }

    private void Update()
    {
        if (launched && Time.time - launchTime >= lifetime)
        {
            Expire();
        }
    }

    public void Launch()
    {
        launched = true;
        launchTime = Time.time;
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnCollision && IsTagInArray(collision.collider.tag))
        {
            onProjectileHitEvent.Invoke();
            DestroyProjectile();
        }
    }

    private void Expire()
    {
        onProjectileExpiredEvent.Invoke();
        DestroyProjectile();
    }

    private bool IsTagInArray(string tagToCheck)
    {
        foreach (string tag in collisionTags)
        {
            if (tag == tagToCheck)
            {
                return true;
            }
        }
        return false;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
