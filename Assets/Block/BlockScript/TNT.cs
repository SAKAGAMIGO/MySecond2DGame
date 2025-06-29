using UnityEngine;
using DG.Tweening;

public class TNT : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] private float explosionForce; // 爆発力
    [SerializeField] private float explosionRadius; // 爆発半径
    [SerializeField] private GameObject m_scoreTextPrefab = default; // スコアテキストプレハブ
    [SerializeField] private Canvas m_canvas = default; // スコアテキストを表示する Canvas

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

        ShowScoreText(800); // スコアテキストを表示
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
            // スコアテキストを生成し、Canvas の子オブジェクトに設定
            GameObject scoreText = Instantiate(m_scoreTextPrefab, m_canvas.transform);

            // 生成したテキストの位置をワールド座標からスクリーン座標へ変換
            Vector3 worldPosition = this.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            scoreText.transform.position = screenPosition;

            // テキストの内容を設定
            var textComponent = scoreText.GetComponent<UnityEngine.UI.Text>();
            if (textComponent != null)
            {
                textComponent.text = $"+{score}";
            }

            // ScoreTextController を追加して制御
            scoreText.AddComponent<ScoreTextController>();
        }
    }
}
