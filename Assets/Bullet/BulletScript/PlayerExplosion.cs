using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : Player
{

    [SerializeField] private float explosionForce; // ������
    [SerializeField] private float explosionRadius; // �������a

    //�����G�t�F�N�g
    [SerializeField] GameObject Explosion;

    /// <summary>��������</summary>
    public override void Detonate()
    {
        // �����͈͓̔��̃I�u�W�F�N�g�����o
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);

        }
        Instantiate(Explosion, transform.position, transform.rotation);
        // ���e�I�u�W�F�N�g��j��
        Destroy(gameObject);
        Debug.Log("bbbb");
    }

    /// <summary>������΂��̏���</summary>
    /// <param name="targetCollider"></param>
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

            Debug.Log("force");
        }
    }
}
