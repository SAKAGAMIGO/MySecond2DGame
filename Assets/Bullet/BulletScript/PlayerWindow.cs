using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindow : Player
{
    public float windForce = 10f; // 風の力
    public float windRange = 5f;  // 風の有効範囲
    private bool windActivated = false; // 風が発生したかどうかのフラグ

    void Update()
    {

    }

    public override void Detonate()
    {
        if (!windActivated)
        {
            // 自身の前方に風を発生させる
            Vector3 windDirection = transform.forward;

            // 範囲内のオブジェクトを検出する
            Collider[] colliders = Physics.OverlapSphere(transform.position, windRange);

            foreach (Collider collider in colliders)
            {
                // オブジェクトにRigidbodyがあれば力を加える
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // 風の方向に力を加える
                    rb.AddForce(windDirection * windForce, ForceMode.Impulse);
                }
            }
            windActivated = true; // 一回だけ発生させるためにフラグをオンにする
        }

        // 風のエフェクトなどの追加処理（必要なら）
        // e.g., ParticleSystemを再生するなど
    }

    void OnDrawGizmosSelected()
    {
        // 範囲の視覚化
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, windRange);
    }
}
