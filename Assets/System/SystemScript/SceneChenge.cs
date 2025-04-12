using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChenge : MonoBehaviour
{
    public static int _stageNumber;

    StarDisplay _starDisplay;

    private void Start()
    {
        _starDisplay = Object.FindObjectOfType<StarDisplay>();
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

    public static readonly Dictionary<SceneKind, string> SceneNames = new Dictionary<SceneKind, string>()
    {
        { SceneKind.Title, "Title" },
        { SceneKind.Select, "Select" },
        { SceneKind.Result, "Result" },
        { SceneKind.GameOver, "GameOver" },
        { SceneKind.Stage1, "Stage1" },
        { SceneKind.Stage2, "Stage2" },
        { SceneKind.Stage3, "Stage3" },
        { SceneKind.Stage4, "Stage4" },
        { SceneKind.Stage5, "Stage5" },
        { SceneKind.Stage6, "Stage6" }
    };

    private void Title() => SceneManager.LoadScene(SceneNames[SceneKind.Title]);
    public void GetTitle() => Invoke(nameof(Title), 2f);

    private void StageSelect() => SceneManager.LoadScene(SceneNames[SceneKind.Select]);
    public void GetStageSelect() => Invoke(nameof(StageSelect), 2f);

    private void Result() => SceneManager.LoadScene(SceneNames[SceneKind.Result]);
    public void GetResult()
    {
        ScoreDisplay.SaveResultScore(); // ƒXƒRƒA•Û‘¶
        Invoke(nameof(Result), 1f);
    }

    private void GameOver() => SceneManager.LoadScene(SceneNames[SceneKind.GameOver]);
    public void GetGameOver() => Invoke(nameof(GameOver), 1f);

    private void Stage1() { SceneManager.LoadScene(SceneNames[SceneKind.Stage1]); _stageNumber = 0; ScoreDisplay.ResetScore(); }
    public void GetStage1() => Invoke(nameof(Stage1), 1f);

    private void Stage2() { SceneManager.LoadScene(SceneNames[SceneKind.Stage2]); _stageNumber = 1; ScoreDisplay.ResetScore(); }
    public void GetStage2() => Invoke(nameof(Stage2), 1f);

    private void Stage3() { SceneManager.LoadScene(SceneNames[SceneKind.Stage3]); _stageNumber = 2; ScoreDisplay.ResetScore(); }
    public void GetStage3() => Invoke(nameof(Stage3), 1f);

    private void Stage4() { SceneManager.LoadScene(SceneNames[SceneKind.Stage4]); _stageNumber = 3; ScoreDisplay.ResetScore(); }
    public void GetStage4() => Invoke(nameof(Stage4), 1f);

    private void Stage5() { SceneManager.LoadScene(SceneNames[SceneKind.Stage5]); _stageNumber = 4; ScoreDisplay.ResetScore(); }
    public void GetStage5() => Invoke(nameof(Stage5), 1f);

    private void Stage6() { SceneManager.LoadScene(SceneNames[SceneKind.Stage6]); _stageNumber = 5; ScoreDisplay.ResetScore(); }
    public void GetStage6() => Invoke(nameof(Stage6), 1f);
}
