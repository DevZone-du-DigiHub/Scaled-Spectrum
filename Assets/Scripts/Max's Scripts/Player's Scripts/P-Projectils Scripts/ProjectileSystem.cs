using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    [Header("Lifetime Settings")]
    [SerializeField] private float lifetime = 5f;

    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
