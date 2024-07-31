using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /// <summary>スコアテキスト</summary>
    [SerializeField] Text _scoreText;

    /// <summary>スコア</summary>
    public static int _score;

    public enum SceneKind
    {
        Title,
        StageSelect,
        Result,
        GameOver,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        Stage6
    }

    public static Dictionary<SceneKind, string> SceneNames = new Dictionary<SceneKind, string>()
    {
        {SceneKind.Title, "TitleScene" },
        {SceneKind.Result, "Result" },
        {SceneKind.GameOver, "GameOver" },
        {SceneKind.Stage1, "Stage1" },
        {SceneKind.Stage2, "Stage2" },
        {SceneKind.Stage3, "Stage3" },
        {SceneKind.Stage4, "Stage4" },
        {SceneKind.Stage5, "Stage5" },
        {SceneKind.Stage6, "Stage6" }
    };

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
    private void Result()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Result]);
    }

    public void GetResult()
    {
        Invoke(nameof(Result), 0.5f);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.GameOver]);
    }

    public void GetGameOver()
    {
        Invoke(nameof(GameOver), 0.5f);
    }

    private void Stage1()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage1]);
    }

}
