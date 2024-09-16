using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Player
{
    public SmallClone _ballPrefab; // ���􂵂Đ������鋅��Prefab
    public int _splitCount = 3; // ���􂷂鐔
    public float _splitAngle = 30f; // �����̋��̊g�U�p�x
    private bool _hasSplit = true;
    private bool _isSplit = false;
    SmallClone[] _smallClone = new SmallClone[99];


    public override void Detonate()
    {
        if (_hasSplit)
        {
            _isSplit = true;
            // ���݂̑��x�ƕ�����ێ�
            Vector3 currentVelocity = _rb.velocity;

            // ���􂳂���
            for (int i = 0; i < _splitCount; i++)
            {

                // Prefab���C���X�^���X�����ĐV�������𐶐�
                var splitBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
                Debug.Log($"splitBall{splitBall}");
                _smallClone[i] = splitBall;

                // �����̋��̕������������ς���
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
