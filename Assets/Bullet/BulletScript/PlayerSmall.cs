using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Player
{
    public GameObject _ballPrefab; // 分裂して生成する球のPrefab
    public int _splitCount = 3; // 分裂する数
    public float _splitAngle = 30f; // 分裂後の球の拡散角度
    private bool _hasSplit = true;
    private bool _isSplit = false;

    public override void Detonate()
    {
        if (_hasSplit)
        {
            _isSplit = true;
            // 現在の速度と方向を保持
            //Vector3 currentVelocity = _rb.velocity;

            // 分裂させる
            for (int i = 0; i < _splitCount; i++)
            {
                // Prefabをインスタンス化して新しい球を生成
                GameObject splitBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity);

                // 新しい球にRigidbodyを取得
                Rigidbody splitRb = splitBall.GetComponent<Rigidbody>();

                // 分裂後の球の方向を少しずつ変える
                float angleOffset = (i - (_splitCount / 2f)) * _splitAngle;
                Quaternion rotation = Quaternion.Euler(0, angleOffset, 0);
                //Vector3 newVelocity = rotation * currentVelocity;

                // 新しい球に勢いを与える
                //splitRb.velocity = newVelocity;
                
                if (_isSplit)
                {
                    AA();
                }
            }
        }
    }
    private void AA()
    {
        _hasSplit = false;
        Destroy(_ballPrefab, 3f);
    }
}
