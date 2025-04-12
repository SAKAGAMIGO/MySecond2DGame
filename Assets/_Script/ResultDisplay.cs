using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    public Text resultScoreText; // リザルト用のスコアテキスト

    private void Start()
    {
        Debug.Log("リザルト表示開始。_resultScore = " + ScoreDisplay._resultScore);
        // リザルト用スコアを表示
        resultScoreText.text = $"Score : {ScoreDisplay._resultScore}";
    }
}
