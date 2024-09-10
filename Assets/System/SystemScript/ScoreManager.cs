using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // シングルトンインスタンス

    /// <summary>スコアテキスト</summary>
    [SerializeField] Text _scoreText;

    /// <summary>スコア</summary>
    public int _score;

    public enum SceneKind
    {
        Title,
        Select,
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
        {SceneKind.Select, "StageSelect"},
        {SceneKind.Result, "Result" },
        {SceneKind.GameOver, "GameOver" },
        {SceneKind.Stage1, "Stage1" },
        {SceneKind.Stage2, "Stage2" },
        {SceneKind.Stage3, "Stage3" },
        {SceneKind.Stage4, "Stage4" },
        {SceneKind.Stage5, "Stage5" },
        {SceneKind.Stage6, "Stage6" }
    };

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // このオブジェクトをシーンが遷移しても破棄しない
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスがある場合は重複を避けるために破棄
        }
    }

    public void Start()
    {
        _scoreText.text = "SCORE" + _score;
        UpdateScoreText();
    }

    /// <summary>Scoreを加算</summary>
    public void AddScore(int points)
    {
        _score += points;
        _scoreText.text = "SCORE" + _score;
        UpdateScoreText ();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score.ToString();
        }
    }

    //public int GetCurrentScore()
    //{
    //    return _score;
    //}
        
    private void Title()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Title]);
    }

    public void GetTitle()
    {
        Invoke(nameof(Title), 2f);
    }
    private void StageSelect()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Select]);
    }

    public void GetStageSelect()
    {
        Invoke(nameof(StageSelect), 3f);
        Debug.Log("ステージセレクトへ");
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
    public void GetStage1()
    {
        Invoke(nameof(Stage1), 1f);
    }

}
