using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>�X�R�A�e�L�X�g</summary>
    public Text _score;

    void Update()
    {
        //static�ϐ�_score�����Z
        _score.text = "SCORE: " + SceneChenge._score;
    }
}
