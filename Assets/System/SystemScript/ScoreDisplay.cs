using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>スコアテキスト</summary>
    public Text _scoreText;

    private void Update()
    {
        // ステージ遷移後でもスコアを表示できるようにする
        _scoreText.text = "SCORE: " + SceneChenge._score;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
