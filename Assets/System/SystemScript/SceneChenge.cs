using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SceneChenge : MonoBehaviour
{
    /// <summary>スコア</summary>
    public static int _score ;
    public static int _stageNumber;
    public static int _resultScore; // リザルト用のスコア

    static bool isInitialized = false;

    static float _scoreChangeDuration = 0.5f; // スコアカウントアップの時間

    StarDisplay _starDisplay;

    private void Start()
    {
        _starDisplay = Object.FindObjectOfType<StarDisplay>();
    }

    private void Update()
    {
       // Debug.Log( _score);
       // Debug.Log(_resultScore);
    }

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
        {SceneKind.Title, "Title" },
        {SceneKind.Select, "Select"},
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
        if (!isInitialized)
        {
            // 初回のみ初期化
            _score = 0;
            _resultScore = 0; 
            isInitialized = true;
        }
    }

    public static void ResetScore()
    {
        _score = 0; // スコアをリセット
        ScoreDisplay.UpdateScoreText(_score); // テキストを更新
        _resultScore = _score;
    }

    public static void AddScore(int points)
    {
        int initialScore = _score; // 現在のスコア
        int targetScore = _score + points; // 目標スコア

        _score = targetScore; // スコアを更新（これによりスコアが保持される）
        _resultScore = _score; // リザルト用スコアも更新

        // UIテキストの更新
        ScoreDisplay.UpdateScoreText(_score);

        // DOTweenでアニメーション
        DOTween.To(
            () => initialScore,
            value =>
            {
                _score = value; // スコアをリアルタイムで更新
                _resultScore = _score; // リザルトに反映
                ScoreDisplay.UpdateScoreText(_score); // テキストも更新
            },
            targetScore,
            0.5f // アニメーション時間（秒）
        );
    }

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
        Invoke(nameof(StageSelect), 2f);
    }

    private void Result()
    {
        _resultScore = _score; // リザルト用スコアを更新
        SceneManager.LoadScene(SceneNames[SceneKind.Result]);
    }

    public void GetResult()
    {
        Invoke(nameof(Result), 1f);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.GameOver]);
    }
    
    public void GetGameOver()
    {
        Invoke(nameof(GameOver), 1f);
    }

    private void Stage1()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage1]);
        _stageNumber = 0;
        ResetScore();
    }
    public void GetStage1()
    {
        Invoke(nameof(Stage1), 1f);
    }

    private void Stage2()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage2]);
        _stageNumber = 1;
        ResetScore();
    }

    public void GetStage2()
    {
        Invoke(nameof(Stage2), 1f);
    }

    private void Stage3()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage3]);
        _stageNumber = 2;
        ResetScore();
    }
    
    public void GetStage3()
    {
        Invoke(nameof(Stage3), 1f);
    }

    private void Stage4()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage4]);
        _stageNumber = 3;
        ResetScore();
    }

    public void GetStage4()
    {
        Invoke(nameof(Stage4), 1f);
    }

    private void Stage5()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage5]);
        _stageNumber = 4;
        ResetScore();
    }

    public void GetStage5()
    {
        Invoke(nameof(Stage5), 1f);
    }

    private void Stage6()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage6]);
        _stageNumber = 5;
        ResetScore();
    }

    public void GetStage6()
    {
        Invoke(nameof(Stage6), 1f);
    }
}
