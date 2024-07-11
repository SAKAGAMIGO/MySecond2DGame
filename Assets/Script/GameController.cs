using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /// <summary>�v���C���[Prefab</summary>
    [SerializeField] Player[] _playerPrefabs;

    /// <summary>�X�R�A�e�L�X�g</summary>
    [SerializeField] Text ScoreText;

    /// <summary>Enemy�̎c�@�e�L�X�g</summary>
    [SerializeField] Text EnemyText;

    /// <summary>�X�R�A</summary>
    int _score;

    /// <summary>Enemy�̎c�@</summary>
    int _enemyScore;

    /// <summary>�t�B�[���h��ɂ���Enemy�̐�
    public GameObject[] enemyBox;

    /// <summary>�I���{�^��</summary>
    [SerializeField] GameObject FinishButtom;

    /// <summary>GameOver�{�^��</summary>
    [SerializeField] GameObject GameOverButton;

    /// <summary>Player�o���J�E���g</summary>
    int count = 0;

    /// <summary>Player�o���^�U</summary>
    public bool isCount = true;

    /// <summary>GameOver�^�U</summary>
    public bool isGameOver = false;

    private void Start()
    {
        enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        ScoreText.text = "SCORE:" + _score;
        EnemyText.text = "ENEMY:" + _enemyScore + "/" + enemyBox.Length;
        FinishButtom.SetActive(false);
        GameOverButton.SetActive(false);
    }

    private void Update()
    {
        //Enemy�̃J�E���g��0�ɂȂ�������s
        GameClear();
        //Player�̃X�|�[��
        PlayerSpawn();

        GameOver();
    }

    public void Count()
    {
        if (isCount = true)
        {
            count++;
        }
    }

    /// <summary>Player�̃X�|�[��</summary>
    private void PlayerSpawn()
    {
        if (_playerPrefabs.Length > count)
        {
            if (isCount == true)
            {
                Instantiate(_playerPrefabs[count], transform.position, Quaternion.identity);
                isCount = false;
                //count = (count + 1) % _playerPrefabs.Length;
                
                
            }
        }
        else if (_playerPrefabs.Length <= count)
        {
            isGameOver = true;
            Debug.LogWarning("GameOver");
            Debug.LogWarning(_playerPrefabs.Length + "to" + count);
        }   
    }

    /// <summary>Score�����Z</summary>
    public void AddScore()
    {
        _score += 500;
        ScoreText.text = "SCORE:" + _score;
    }

    /// <summary>Enemy�̐�</summary>
    public void EnemyScore()
    {
        _enemyScore += 1;
        EnemyText.text = "ENEMY:" + _enemyScore + "/" + enemyBox.Length;
    }

    /// <summary>FinishButtom��\��</summary>
    private void GameClear()
    {
        if (_enemyScore >= enemyBox.Length)
        {
            FinishButtom.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            GameOverButton.SetActive(true);
        }
    }

    /// <summary>���U���g��ʂփ��[�h</summary>
    void Result()
    {
        SceneManager.LoadScene("Result");
    }
        
    /// <summary>0.5�b��ɍ쓮</summary>
    public void GetStage1()
    {
        Invoke(nameof(Result), 0.5f);
    }
}
