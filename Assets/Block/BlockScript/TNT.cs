using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TNT : MonoBehaviour
{
    ScoreManager _scoreManager;

    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // ������
    [SerializeField] private float explosionRadius; // �������a

    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 15;


    void Start()
    {
        _scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        DieVelocity--;
        //���ʑ��x��10��菬�����Ȃ�ƍ��F�ɂȂ�
        if (DieVelocity <= 10f)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Destroy(gameObject);
        }
    }

    // ��������
    public void Detonate()
    {

        // �����͈͓̔��̃I�u�W�F�N�g�����o
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);
        }
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Bomb);
        Detonate();
        Instantiate(explosion,transform.position,transform.rotation);
        _scoreManager.AddScore(800);
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
