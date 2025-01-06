using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 10;

    GameController gameController;

    [SerializeField] GameObject m_effect = default;
    [SerializeField] GameObject m_scoreTextPrefab = default; // スコアテキストのプレハブ
    [SerializeField] Canvas m_canvas = default; // スコアテキストを表示するキャンバス

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>衝突イベント</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            SceneChenge.AddScore(1000);
            ShowScoreText(1000); // スコアテキストを表示
            Hit();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameController.EnemyScore();
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
