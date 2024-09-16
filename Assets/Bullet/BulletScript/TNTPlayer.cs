using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTPlayer : Player
{
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // 爆発力
    [SerializeField] private float explosionRadius; // 爆発半径
    GameController _gameController;

    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 15;

    // 爆発処理
    public override void Detonate()
    {
        // 爆風の範囲内のオブジェクトを検出
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

        _animator.Play("Angry");

        // 爆弾オブジェクトを破棄
        Destroy(gameObject);
    }

   protected override void OnDestroy()
    {
        Detonate();
        Instantiate(explosion, transform.position, transform.rotation);
        _gameController = FindAnyObjectByType<GameController>();
        _gameController._isPlayerCount = true;
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
