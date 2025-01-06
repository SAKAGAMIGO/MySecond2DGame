using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    /// <summary>�X�R�A�e�L�X�g</summary>
    public Text _scoreText;

    private static ScoreDisplay instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �I�u�W�F�N�g��ێ�
        }
        else
        {
            Destroy(gameObject); // �d������I�u�W�F�N�g��j��
        }
    }

    /// <summary>�X�R�A�e�L�X�g���X�V</summary>
    public static void UpdateScoreText(int score)
    {
        if (instance == null)
        {
            Debug.LogWarning("ScoreDisplay instance is null!");
            return;
        }

        if (instance._scoreText == null)
        {
            // �Ď擾�����݂�
            instance._scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
            if (instance._scoreText == null)
            {
                Debug.LogWarning("ScoreText is not found in the scene!");
                return;
            }
        }

        instance._scoreText.text = $"SCORE: {score}";
    }

}
