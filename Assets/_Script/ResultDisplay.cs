using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    public Text resultScoreText; // ���U���g�p�̃X�R�A�e�L�X�g

    private void Start()
    {
        // ���U���g�p�X�R�A��\��
        resultScoreText.text = $"Score : {SceneChenge._resultScore}";
    }
}
