using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour
{
    public Text _scoreText;

    private static ScoreDisplay instance;

    private int _score = 0;
    public static int _resultScore = 0;

    private const float _scoreChangeDuration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �I�u�W�F�N�g��ێ�
            Debug.Log("ScoreDisplay is now alive and marked as DontDestroyOnLoad");
        }
        else
        {
            Debug.Log("Duplicate ScoreDisplay destroyed");
            Destroy(gameObject); // �d������I�u�W�F�N�g��j��
        }

        // _scoreText ���܂��ݒ肳��Ă��Ȃ��ꍇ�ɁA�V�[������T���Đݒ肷��
        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("TextCanvas")?.
                transform.Find("ScoreText")?.GetComponent<Text>();
            if (_scoreText == null)
            {
                Debug.LogWarning("ScoreText is not found in the scene!");
            }
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
            instance._scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
            if (instance._scoreText == null)
            {
                Debug.LogWarning("ScoreText is not found in the scene!");
                return;
            }
        }

        instance._scoreText.text = $"SCORE: {score}";
    }

    /// <summary>�X�R�A�����Z�b�g</summary>
    public static void ResetScore()
    {
        if (instance == null) return;

        instance._score = 0;
        _resultScore = 0;
        UpdateScoreText(instance._score);
    }

    /// <summary>�X�R�A�����Z</summary>
    public static void AddScore(int points)
    {
        if (instance == null) return;

        int initialScore = instance._score;
        int targetScore = instance._score + points;

        instance._score = targetScore;
        _resultScore = targetScore;

        UpdateScoreText(targetScore);

        DOTween.To(() => initialScore, value =>
        {
            instance._score = value;
            _resultScore = value;
            UpdateScoreText(value);
        }, targetScore, _scoreChangeDuration);
    }

    /// <summary>���U���g�p�X�R�A���擾</summary>
    public static void SaveResultScore()
    {
        if (instance == null) return;

        _resultScore = instance._score;
    }
}
