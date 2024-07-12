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

    /// <summary>Player�o���J�E���g</summary>
    int _playercount = 0;

    /// <summary>Player�o���^�U</summary>
    public bool IsPlayerCount = true;

    /// <summary>�t�B�[���h��ɂ���Enemy�̐�
    [SerializeField] GameObject[] enemyBox;

    /// <summary>�X�R�A�e�L�X�g</summary>
    [SerializeField] Text ScoreText;

    /// <summary>�X�R�A</summary>
    int _score;

    /// <summary>Enemy�̎c�@�e�L�X�g</summary>
    [SerializeField] Text EnemyText;

    /// <summary>Enemy�̎c�@</summary>
    int _enemyScore;

    /// <summary>Finish�^�U</summary>
    private bool isFinish = false;

    /// <summary>�I���{�^��</summary>
    [SerializeField] GameObject FinishButtom;

    /// <summary>GameOver�^�U</summary>
    private bool isGameOver = false;

    /// <summary>GameOver�{�^��</summary>
    [SerializeField] GameObject GameOverButton;

    /// <summary>�����Ă���A�C�e���̃��X�g</summary>
    List<ItemBaceClass> _itemList = new List<ItemBaceClass>();

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

        // �A�C�e�����g��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_itemList.Count > 0)
            {
                // ���X�g�̐擪�ɂ���A�C�e�����g���āA�j������
                ItemBaceClass item = _itemList[0];
                _itemList.RemoveAt(0);
                item.Activate();
                Destroy(item.gameObject);
                Debug.Log("�A�C�e���g�p");
            }
        }
    }

    /// <summary>
    /// �A�C�e�����A�C�e�����X�g�ɒǉ�����
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBaceClass item)
    {
        _itemList.Add(item);
        Debug.Log(item);
    }

    public void Count()
    {
        if (IsPlayerCount = true)
        {
            _playercount++;
        }
    }

    /// <summary>Player�̃X�|�[��</summary>
    private void PlayerSpawn()
    {
        if (_playerPrefabs.Length > _playercount)
        {
            if (IsPlayerCount == true)
            {
                Instantiate(_playerPrefabs[_playercount], transform.position, Quaternion.identity);
                IsPlayerCount = false; 
            }
        }
        else if (_playerPrefabs.Length <= _playercount)
        {
            isGameOver = true;
            Debug.LogWarning("GameOver");
            Debug.LogWarning(_playerPrefabs.Length + "to" + _playercount);
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
        /// <summary>�X�R�A�e�L�X�g��Enemy�̐�����������</summary>
        if (_enemyScore >= enemyBox.Length)
        {
            isFinish = true;
        }
    }

    /// <summary>FinishButtom��\��</summary>
    private void GameClear()
    {
        if ( isFinish)
        {
            FinishButtom.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (isGameOver && isFinish == false)
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
