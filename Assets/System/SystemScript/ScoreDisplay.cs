using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>スコアテキスト</summary>
    public Text _score;

    void Update()
    {
        //static変数_scoreを加算
        _score.text = "SCORE: " + SceneChenge._score;
    }
}
