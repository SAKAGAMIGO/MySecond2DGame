using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>スコアテキスト</summary>
    public Text _scoreText;

    private static ScoreDisplay instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // オブジェクトを保持
        }
        else
        {
            Destroy(gameObject); // 重複するオブジェクトを破棄
        }
    }

    /// <summary>スコアテキストを更新</summary>
    public static void UpdateScoreText(int score)
    {
        if (instance == null)
        {
            Debug.LogWarning("ScoreDisplay instance is null!");
            return;
        }

        if (instance._scoreText == null)
        {
            // 再取得を試みる
            instance._scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
            if (instance._scoreText == null)
            {
                Debug.LogWarning("ScoreText is not found in the scene!");
                return;
            }
        }

        instance._scoreText.text = $"SCORE: {score}";
    }

}
