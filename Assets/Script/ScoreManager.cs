using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /// <summary>スコアテキスト</summary>
    [SerializeField] Text _scoreText;

    /// <summary>スコア</summary>
    public static int _score;

    void Start()
    {
        _scoreText.text = $"SCORE:{_score}";
    }

    /// <summary>Scoreを加算</summary>
    public void AddScore(int Value)
    {
        _score += Value;
        _scoreText.text = $"SCORE:{_score}";
    }
}
