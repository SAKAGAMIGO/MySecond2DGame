using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /// <summary>�X�R�A�e�L�X�g</summary>
    [SerializeField] Text _scoreText;

    /// <summary>�X�R�A</summary>
    public static int _score;

    void Start()
    {
        _scoreText.text = $"SCORE:{_score}";
    }

    /// <summary>Score�����Z</summary>
    public void AddScore(int Value)
    {
        _score += Value;
        _scoreText.text = $"SCORE:{_score}";
    }
}
