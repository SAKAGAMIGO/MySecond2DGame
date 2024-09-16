using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Player
{
    public SmallClone _ballPrefab; // 分裂して生成する球のPrefab
    public int _splitCount = 3; // 分裂する数
    public float _splitAngle = 30f; // 分裂後の球の拡散角度
    private bool _hasSplit = true;
    private bool _isSplit = false;
    SmallClone[] _smallClone = new SmallClone[99];


    public override void Detonate()
    {
        if (_hasSplit)
        {
            _isSplit = true;
            // 現在の速度と方向を保持
            Vector3 currentVelocity = _rb.velocity;

            // 分裂させる
            for (int i = 0; i < _splitCount; i++)
            {

                // Prefabをインスタンス化して新しい球を生成
                var splitBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
                Debug.Log($"splitBall{splitBall}");
                _smallClone[i] = splitBall;

                // 分裂後の球の方向を少しずつ変える
                float angleOffset = (i - (_splitCount / 2f)) * _splitAngle;
                Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
                Vector3 newVelocity = rotation * currentVelocity;

                // splitBall.transform.rotation = rotation;
                splitBall.SetVelocity(newVelocity);

                if (_isSplit)
                {
                    _hasSplit = false;
                    Destroy(splitBall, 3f);
                }
              
            }
            
            _animator.Play("Angry");
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (_smallClone != null)
        {
            foreach (var pair in _smallClone)
            {
                if (pair != null && pair.gameObject != null)
                {
                    Destroy(pair.gameObject);
                }
            }
        }
    }
}
