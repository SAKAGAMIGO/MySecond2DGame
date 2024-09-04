using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : Player
{

    [SerializeField] private float explosionForce; // 爆発力
    [SerializeField] private float explosionRadius; // 爆発半径

    //爆発エフェクト
    [SerializeField] GameObject Explosion;

    /// <summary>爆発処理</summary>
    public override void Detonate()
    {
        // 爆風の範囲内のオブジェクトを検出
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);

        }
        Instantiate(Explosion, transform.position, transform.rotation);
        // 爆弾オブジェクトを破棄
        Destroy(gameObject);
        Debug.Log("bbbb");
    }

    /// <summary>吹き飛ばしの処理</summary>
    /// <param name="targetCollider"></param>
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

            Debug.Log("force");
        }
    }
}
