using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ScoreManager;

public class GameController : MonoBehaviour
{
    [SerializeField] Player _tntPlayer;

    bool _isTNT = false;

    /// <summary>Player�o���J�E���g</summary>
    int _playercount = 0;

    /// <summary>Player�o���^�U</summary>
    public bool IsPlayerCount = true;


    /// <summary>Enemy�̎c�@�e�L�X�g</summary>
    [SerializeField] Text _enemyText;

    /// <summary>Enemy�̎c�@</summary>
    int _enemyScore;

    /// <summary>Finish�^�U</summary>
    private bool _isFinish = false;

    /// <summary>�I���{�^��</summary>
    [SerializeField] GameObject _finishButtom;

    /// <summary>GameOver�^�U</summary>
    private bool _isGameOver = false;

    /// <summary>GameOver�{�^��</summary>
    [SerializeField] GameObject _gameOverButton;

    /// <summary>�t�B�[���h��ɂ���Enemy�̐�
    [SerializeField] GameObject[] _enemyBox;

    /// <summary>�v���C���[Prefab</summary>
    [SerializeField] List<Player> _playerList = new List<Player>();

    //���ݏo�����Ă��v���C���[�̃v���t�@�u
    [SerializeField] Player _currentPlayerPrefab;

    //���݂̃v���C���[
    [SerializeField] Player _currentPlayer;

    /// <summary>�L�[��ItemType, �l��ItemBaceClass</summary>
    [SerializeField] Dictionary<ItemType, ItemBaceClass> _itemDic = new Dictionary<ItemType, ItemBaceClass>();
    /// <summary>�L�[��ItemType, �l��Int</summary>
    [SerializeField] Dictionary<ItemType, int> _itemCount = new Dictionary<ItemType, int>();

    public void Start()
    {
        _enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyText.text = "ENEMY:" + _enemyScore + "/" + _enemyBox.Length;
        _finishButtom.SetActive(false);
        _gameOverButton.SetActive(false);
    }

    private void Update()
    {
        //Enemy�̃J�E���g��0�ɂȂ�������s
        GameClear();

        //Player�̃X�|�[��
        GetPlayerSpawn();

        GameOver();
    }

    /// <summary>
    /// �A�C�e�����A�C�e�����X�g�ɒǉ�����
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemType itemType, ItemBaceClass item)
    {
        //_itemDic��itemType��������(ContainsKey = bool�^)
        if (_itemDic.ContainsKey(itemType))
        {
             _itemCount[itemType]++;
            Debug.Log("�A�C�e���擾" + _itemCount);
        }

        else
        {
            _itemDic.Add(itemType, item);
            _itemCount.Add(itemType, 1);
        }
    }

    // �A�C�e�����g��
    public void UseItem(ItemType itemType)
    {
        if (_itemDic.Count > 0)
        {
            if (_itemDic[itemType] == null || _itemCount.Count == 0)
            {
                return;
            }

            // item�� _itemDic���i�[
            ItemBaceClass item = _itemDic[itemType];

            item.Activate();
        }
            _itemCount[itemType]--;
            Debug.Log("�A�C�e���g�p" + _itemCount);
    }

    public void AddTNT()
    {
        //���ݏo�����Ă��v���C���[�̃v���t�@�u���O�Ԗڂɏo��
        _playerList.Insert(0, _currentPlayerPrefab);
        //TNT�v���C���[���O�Ԗڂɏo��
        _playerList.Insert(0, _tntPlayer);
        //���݂̃v���C���[��j��
        Destroy(_currentPlayer.gameObject);
    }


    /// <summary>Player�̃X�|�[��</summary>
    private void PlayerSpawn()
    {
        if (_playerList.Count > 0)
        {
            if (IsPlayerCount == true)
            {
                //���ݏo�����Ă��v���C���[�̃v���t�@�u�Ƀv���C���[���X�g��0�Ԗڂ��i�[
                _currentPlayerPrefab = _playerList[0];
                GameObject obj = Instantiate(_playerList[0].gameObject, transform.position, Quaternion.identity);
                _currentPlayer = obj.GetComponent<Player>();
                //�v���C���[���X�g��0�Ԗڂ�j��
                _playerList.Remove(_playerList[0]);
                IsPlayerCount = false;
            }
        }
        else if (_currentPlayer = null)
        {
            _isGameOver = true;
            Debug.LogWarning("GameOver");
            Debug.LogWarning(_playerList.Count + "to" + _playercount);
        }
    }

    private void GetPlayerSpawn()
    {
        Invoke(nameof(PlayerSpawn), 1f);
    }

    /// <summary>Enemy�̐�</summary>
    public void EnemyScore()
    {
        _enemyScore += 1;
        _enemyText.text = "ENEMY:" + _enemyScore + "/" + _enemyBox.Length;
        /// <summary>�X�R�A�e�L�X�g��Enemy�̐�����������</summary>
        if (_enemyScore >= _enemyBox.Length)
        {
            _isFinish = true;
        }
    }

    /// <summary>FinishButtom��\��</summary>
    private void GameClear()
    {
        if (_isFinish)
        {
            _finishButtom.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (_isGameOver && _isFinish == false)
        {
            _gameOverButton.SetActive(true);
        }
    }

    /// <summary>���U���g��ʂփ��[�h</summary>
    void Result()
    {
        SceneManager.LoadScene("Result");
    }

    /// <summary>0.5�b��ɍ쓮</summary>
    public void GetResult()
    {
        Invoke(nameof(Result), 2f);
    }
}
