using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /// <summary>スコア</summary>
    public static int _score = 0;

    [SerializeField,Tooltip("スター表示のためのノルマスコア1")] int _quotaScoreOne;
    [SerializeField,Tooltip("スター表示のためのノルマスコア2")] int _quotaScoreTwo;
    [SerializeField,Tooltip("スター表示のためのノルマスコア3")] int _quotaScoreThree;

    [SerializeField] GameObject _starOne;
    [SerializeField] GameObject _starTwo;
    [SerializeField] GameObject _starThree;

    private void Start()
    {
        StartCoroutine(StarAnim());
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


    /// <summary>Scoreを加算</summary>
    public static void AddScore(int points)
    {
        _score += points;
    }

    IEnumerator StarAnim()
    {
        if (_score < _quotaScoreOne)
        {
            yield break;
        }
        _starOne.GetComponent<Animator>().Play("StarScore_1");
        yield return new WaitForSeconds(0.5f);
        if (_score < _quotaScoreTwo)
        {
            yield break;
        }
        _starTwo.GetComponent<Animator>().Play("StarScpre_2");
        yield return new WaitForSeconds(0.5f);
        if (_score < _quotaScoreThree)
        {
            yield break;
        }
        _starThree.GetComponent<Animator>().Play("StarScore_3");
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
        Invoke(nameof(StageSelect), 1f);
        Debug.Log("ステージセレクトへ");
    }

    private void Result()
    {
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
    }
    public void GetStage1()
    {
        Invoke(nameof(Stage1), 1f);
    }

    private void Stage2()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage2]);
    }

    public void GetStage2()
    {
        Invoke(nameof(Stage2), 1f);
    }

    private void Stage3()
    {
        SceneManager.LoadScene(SceneNames[SceneKind.Stage3]);
    }

    public void GetStage3()
    {
        Invoke(nameof(Stage3), 1f);
    }
}
