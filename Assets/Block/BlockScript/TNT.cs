using UnityEngine;
using DG.Tweening;

public class TNT : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // ������
    [SerializeField] private float explosionRadius; // �������a
    [SerializeField] private GameObject m_scoreTextPrefab = default; // �X�R�A�e�L�X�g�v���n�u
    [SerializeField] private Canvas m_canvas = default; // �X�R�A�e�L�X�g��\������ Canvas

    public float DieVelocity = 15;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Destroy(gameObject);
        }
    }

    public void Detonate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Detonate();
        Instantiate(explosion, transform.position, transform.rotation);
        ScoreDisplay.AddScore(800);

        ShowScoreText(800); // �X�R�A�e�L�X�g��\��
    }

    void ApplyExplosionForce(Collider2D targetCollider)
    {
        Rigidbody2D targetRigidbody = targetCollider.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            Vector2 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
        }
    }

    private void ShowScoreText(int score)
    {
        if (m_scoreTextPrefab && m_canvas)
        {
            // �X�R�A�e�L�X�g�𐶐����ACanvas �̎q�I�u�W�F�N�g�ɐݒ�
            GameObject scoreText = Instantiate(m_scoreTextPrefab, m_canvas.transform);

            // ���������e�L�X�g�̈ʒu�����[���h���W����X�N���[�����W�֕ϊ�
            Vector3 worldPosition = this.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            scoreText.transform.position = screenPosition;

            // �e�L�X�g�̓��e��ݒ�
            var textComponent = scoreText.GetComponent<UnityEngine.UI.Text>();
            if (textComponent != null)
            {
                textComponent.text = $"+{score}";
            }

            // ScoreTextController ��ǉ����Đ���
            scoreText.AddComponent<ScoreTextController>();
        }
    }
}
