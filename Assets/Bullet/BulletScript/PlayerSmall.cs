using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Player
{
    public GameObject _ballPrefab; // ���􂵂Đ������鋅��Prefab
    public int _splitCount = 3; // ���􂷂鐔
    public float _splitAngle = 30f; // �����̋��̊g�U�p�x
    private bool _hasSplit = true;
    private bool _isSplit = false;

    public override void Detonate()
    {
        if (_hasSplit)
        {
            _isSplit = true;
            // ���݂̑��x�ƕ�����ێ�
            //Vector3 currentVelocity = _rb.velocity;

            // ���􂳂���
            for (int i = 0; i < _splitCount; i++)
            {
                // Prefab���C���X�^���X�����ĐV�������𐶐�
                GameObject splitBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity);

                // �V��������Rigidbody���擾
                Rigidbody splitRb = splitBall.GetComponent<Rigidbody>();

                // �����̋��̕������������ς���
                float angleOffset = (i - (_splitCount / 2f)) * _splitAngle;
                Quaternion rotation = Quaternion.Euler(0, angleOffset, 0);
                //Vector3 newVelocity = rotation * currentVelocity;

                // �V�������ɐ�����^����
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
