using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TNT : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // 爆発力
    [SerializeField] private float explosionRadius; // 爆発半径

    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 15;

    /// <summary>衝突イベント</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        DieVelocity--;

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Destroy(gameObject);
        }
    }

    // 爆発処理
    public void Detonate()
    {
        // 爆風の範囲内のオブジェクトを検出
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.TNT);
        Detonate();
        Instantiate(explosion,transform.position,transform.rotation);
        SceneChenge.AddScore(800);
    }

    // 吹き飛ばしの処理
    void ApplyExplosionForce(Collider2D targetCollider)
    {
        Rigidbody2D targetRigidbody = targetCollider.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            // 爆心からの距離に応じて力を計算
            Vector2 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            // 力を加える
            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
        }
    }
}
