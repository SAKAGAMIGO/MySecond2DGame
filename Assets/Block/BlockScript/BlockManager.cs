using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 20;

    [SerializeField] private GameObject m_effect = default;
    [SerializeField] private GameObject m_scoreTextPrefab = default; // スコアテキストのプレハブ
    [SerializeField] private Canvas m_canvas = default;             // スコアテキストを表示するCanvas

    /// <summary>衝突イベント</summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if (DieVelocity <= 8f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Hit();
        }
    }

    public void OnDestroy()
    {
        ScoreDisplay.AddScore(500);
        ShowScoreText(500); // スコアテキストを表示
    }

    private void Hit()
    {
        if (m_effect)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    private void ShowScoreText(int score)
    {
        if (m_scoreTextPrefab && m_canvas)
        {
            // スコアテキストを生成し、Canvas の子オブジェクトに設定
            GameObject scoreText = Instantiate(m_scoreTextPrefab, m_canvas.transform);

            // 生成したテキストの位置をブロックのワールド座標からスクリーン座標へ変換
            Vector3 worldPosition = this.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            scoreText.transform.position = screenPosition;

            // テキストの内容を設定
            var textComponent = scoreText.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = $"+{score}";
            }

            // ScoreTextController を追加して制御
            scoreText.AddComponent<ScoreTextController>();
        }
    }
}
