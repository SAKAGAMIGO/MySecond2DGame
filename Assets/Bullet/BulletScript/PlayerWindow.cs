using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindow : Player
{
    public float windForce = 10f; // ���̗�
    public float windRange = 5f;  // ���̗L���͈�
    private bool windActivated = false; // ���������������ǂ����̃t���O

    void Update()
    {

    }

    public override void Detonate()
    {
        if (!windActivated)
        {
            // ���g�̑O���ɕ��𔭐�������
            Vector3 windDirection = transform.forward;

            // �͈͓��̃I�u�W�F�N�g�����o����
            Collider[] colliders = Physics.OverlapSphere(transform.position, windRange);

            foreach (Collider collider in colliders)
            {
                // �I�u�W�F�N�g��Rigidbody������Η͂�������
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // ���̕����ɗ͂�������
                    rb.AddForce(windDirection * windForce, ForceMode.Impulse);
                }
            }
            windActivated = true; // ��񂾂����������邽�߂Ƀt���O���I���ɂ���
        }

        // ���̃G�t�F�N�g�Ȃǂ̒ǉ������i�K�v�Ȃ�j
        // e.g., ParticleSystem���Đ�����Ȃ�
    }

    void OnDrawGizmosSelected()
    {
        // �͈͂̎��o��
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, windRange);
    }
}
