using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>�X�R�A�e�L�X�g</summary>
    public Text _scoreText;

    void Update()
    {
        _scoreText.text = "SCORE: " + ScoreManager._score;
    }
}
