using UnityEngine;
using DG.Tweening;

public class TNT : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // ”š”­—Í
    [SerializeField] private float explosionRadius; // ”š”­”¼Œa

    public float DieVelocity = 15;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Destroy(gameObject);
        }
    }

    public void Detonate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Detonate();
        Instantiate(explosion, transform.position, transform.rotation);
    }

    void ApplyExplosionForce(Collider2D targetCollider)
    {
        Rigidbody2D targetRigidbody = targetCollider.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            Vector2 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
        }
    }
}
