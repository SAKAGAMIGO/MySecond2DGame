using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Player _tntPlayer;

    bool _isTNT = false;

    /// <summary>Player�o���J�E���g</summary>
    int _playercount = 0;

    /// <summary>Player�o���^�U</summary>
    public bool IsPlayerCount = true;

    /// <summary>�t�B�[���h��ɂ���Enemy�̐�
    [SerializeField] GameObject[] _enemyBox;

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

    /// <summary>Zoom�{�^��</summary>
    [SerializeField] GameObject _zoomButton;
    [SerializeField] GameObject _outButton;

    /// <summary>Zoom,Out�^�U</summary>
    bool _isZoom;
    bool _isOut;

    /// <summary>VacualCamera</summary>
    [SerializeField] CinemachineVirtualCamera _vCamera;

    /// <summary>�v���C���[Prefab</summary>
    [SerializeField] List<Player> _playerList = new List<Player>();

    /// <summary>�����Ă���A�C�e���̃��X�g</summary>
    [SerializeField] List<ItemBaceClass> _itemList = new List<ItemBaceClass>();

    public void Start()
    {
        _enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyText.text = "ENEMY:" + _enemyScore + "/" + _enemyBox.Length;
        _finishButtom.SetActive(false);
        _gameOverButton.SetActive(false);
        _zoomButton.SetActive(true);
        _outButton.SetActive(false);
        _isZoom = true;
        _isOut = false;
    }

    private void Update()
    {
        //Enemy�̃J�E���g��0�ɂȂ�������s
        GameClear();

        //Player�̃X�|�[��
        GetPlayerSpawn();

        GameOver();

        Zoom();
        Out();
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

    // �A�C�e�����g��
    public void UseItem()
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
    
    public void UseTNT()
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

    public void AddTNT()
    {
        _playerList.Add(_tntPlayer);
    }

    public void SpawnCount()
    {
        if (IsPlayerCount = true)
        {
            _playercount++;
        }
    }

    /// <summary>Player�̃X�|�[��</summary>
    private void PlayerSpawn()
    {
        if (_playerList.Count > _playercount)
        {
            if (IsPlayerCount == true)
            {
                Instantiate(_playerList[_playercount], transform.position, Quaternion.identity);
                IsPlayerCount = false;
            }
        }
        else if (_playerList.Count <= _playercount)
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

    //Zoom�{�^���A�N�e�B�u�Ǘ�
    public void Zoom()
    {
        if (_isZoom && _isOut == false)
        {
            _zoomButton.SetActive(true);
        }
        else
        {
            _zoomButton.SetActive(false);
        }
    }

    //Out�{�^���A�N�e�B�u�Ǘ�
    public void Out()
    {
        if (_isOut && _isZoom == false)
        {
            _outButton.SetActive(true);
        }
        else
        {
            _outButton.SetActive(false);
        }
    }

    //VCameraIdol�̗D��x�ύX
    public void ZoomCamera()
    {
        _vCamera.Priority = 0;
        _isZoom = false;
        _isOut = true;
    }
    public void OutCamera()
    {
        _vCamera.Priority = 20;
        _isOut = false;
        _isZoom = true;
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
