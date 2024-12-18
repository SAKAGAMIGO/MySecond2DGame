using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>�X�R�A�e�L�X�g</summary>
    public Text _scoreText;

    private void Update()
    {
        // �X�e�[�W�J�ڌ�ł��X�R�A��\���ł���悤�ɂ���
        _scoreText.text = "SCORE: " + SceneChenge._score;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
