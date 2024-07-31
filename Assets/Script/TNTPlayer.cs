using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTPlayer : Player
{
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // ������
    [SerializeField] private float explosionRadius; // �������a
    GameController _gameController;

    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 15;

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;
        //���ʑ��x��10��菬�����Ȃ�ƍ��F�ɂȂ�
        if (DieVelocity <= 10f)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Debug.Log(collision.relativeVelocity.sqrMagnitude);
            Destroy(this.gameObject);
        }
    }

    // ��������
    public override void Detonate()
    {
        // �����͈͓̔��̃I�u�W�F�N�g�����o
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

        // ���e�I�u�W�F�N�g��j��
        Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        Detonate();
        Instantiate(explosion, transform.position, transform.rotation);
        _gameController = FindAnyObjectByType<GameController>();
        _gameController.IsPlayerCount = true;
    }

    // ������΂��̏���
    void ApplyExplosionForce(Collider2D targetCollider)
    {
        Rigidbody2D targetRigidbody = targetCollider.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            // ���S����̋����ɉ����ė͂��v�Z
            Vector2 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            // �͂�������
            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
        }
    }
}
