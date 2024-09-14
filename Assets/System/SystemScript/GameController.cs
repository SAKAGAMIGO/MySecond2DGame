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
    [SerializeField] Player _tntBullet;

    bool _isTNT = false;

    /// <summary>Player�o���J�E���g</summary>
    int _playercount = 0;

    /// <summary>Player�o���^�U</summary>
    public bool _isPlayerCount = true;

    /// <summary>Enemy�̎c�@�e�L�X�g</summary>
    [SerializeField] Text _enemyText;

    /// <summary>Enemy�̎c�@</summary>
    public int _enemyScore;

    /// <summary>Finish�^�U</summary>
    public bool _isFinish = false;

    /// <summary>�I���{�^��</summary>
    [SerializeField] GameObject _finishButtom;

    /// <summary>GameOver�^�U</summary>
    public bool _isGameOver = false;

    /// <summary>GameOver�{�^��</summary>
    [SerializeField] GameObject _gameOverButton;

    /// <summary>�t�B�[���h��ɂ���Enemy�̐�</summary>
    [SerializeField] GameObject[] _enemyBox;

    //<summary>���݂�Enemy�̐�</summary>
    public int _enemyElementCount;

    /// <summary>�v���C���[Prefab</summary>
    [SerializeField] List<Player> _playerList = new List<Player>();

    //���ݏo�����Ă��v���C���[�̃v���t�@�u
    [SerializeField] Player _currentPlayerPrefab;

    //���݂̃v���C���[
    [SerializeField] Player _currentPlayer;

    /// <summary>�L�[��ItemType, �l��ItemBaceClass</summary>
    [SerializeField] Dictionary<ItemType, ItemBaceClass> _itemDic = new Dictionary<ItemType, ItemBaceClass>();
    /// <summary>�L�[��ItemType, �l��Int</summary>
    public Dictionary<ItemType, int> _itemCount = new Dictionary<ItemType, int>();

    [SerializeField] GameObject _pawerItemButton;
    [SerializeField] GameObject _tntButtom;
    [SerializeField] GameObject _sightButtom;

    /// <summary></summary>
    [SerializeField] Vector2 _taregetPos;
    /// <summary>GameController��_taregetPos�^�U</summary>
    public bool _isTareget;

    bool _isCurrentPlayerMove = false;

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

        if (_isPlayerCount == true)
        {
            //Player�̃X�|�[��
            GetPlayerSpawn();
        }

        GameOver();

        if (_enemyElementCount <= _enemyScore)
        {
            _isTareget = true;
            transform.position = _taregetPos;
        }
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
            Debug.Log("�A�C�e���擾" + _itemCount + item);
        }

        if (_itemDic.ContainsKey(ItemType.Fly))
        {
            _pawerItemButton.gameObject.SetActive(true);
        }

        if (_itemDic.ContainsKey(ItemType.TNT))
        {
            _tntButtom.gameObject.SetActive(true);
        }

        if (_itemDic.ContainsKey(ItemType.Sight))
        {
            _sightButtom.gameObject.SetActive(true);
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
        //if (_itemDic.Count > 0)
        //{
        //    if (_itemDic[itemType] == null || _itemCount.Count > 0)
        //    {
        //        return;
        //    }

        //    // item�� _itemDic���i�[
        //    ItemBaceClass item = _itemDic[itemType];

        //    item.Activate();
        //}
        //_itemCount[itemType]--;
        //Debug.Log("�A�C�e���g�p" + _itemCount);

        // ��������܂��̓A�C�e�������݂��Ȃ��ꍇ�͉������Ȃ�
        if (_itemDic.Count == 0 || !_itemDic.ContainsKey(itemType))
        {
            Debug.Log("�A�C�e�������݂��܂���B");
            return;
        }

        // �w�肵���A�C�e����null�܂��̓J�E���g��0�ȉ��̏ꍇ�͉������Ȃ�
        if (_itemDic[itemType] == null || !_itemCount.ContainsKey(itemType) || _itemCount[itemType] <= 0)
        {
            Debug.Log("�A�C�e�������݂��Ȃ����A�J�E���g��0�ł��B");
            return;
        }

        // item�� _itemDic[itemType] ���i�[
        ItemBaceClass item = _itemDic[itemType];

        // �A�C�e���̃A�N�e�B�u��
        item.Activate();

        // �A�C�e���J�E���g��0�ɂȂ����玫������폜����i�K�v�ɉ����āj
        if (_itemCount[itemType] == 0)
        {
            _itemCount.Remove(itemType);
            Debug.Log($"�A�C�e�� {itemType} �̃J�E���g��0�ɂȂ����̂ō폜���܂����B");
        }
        else
        {
            Debug.Log($"�A�C�e�� {itemType} ���g�p���܂����B�c��J�E���g: {_itemCount[itemType]}");
        }
    }

    public void AddTNT()
    {
        //���ݏo�����Ă��v���C���[�̃v���t�@�u���O�Ԗڂɏo��
        _playerList.Insert(0, _currentPlayerPrefab);
        //TNT�v���C���[���O�Ԗڂɏo��
        _playerList.Insert(0, _tntBullet);
        //���݂̃v���C���[��j��
        Destroy(_currentPlayer.gameObject);
    }


    /// <summary>Player�̃X�|�[��</summary>
    private void PlayerSpawn()
    {
        if (_isPlayerCount == true)
        {
            // ���ݏo�����Ă���v���C���[�̃v���t�@�u�Ƀv���C���[���X�g��0�Ԗڂ��i�[
            _currentPlayerPrefab = _playerList[0];

            // �v���C���[�I�u�W�F�N�g���X�|�i�[�̈ʒu�ɐ���
            GameObject obj = Instantiate(_playerList[0].gameObject, transform.position, Quaternion.identity);

            // �v���C���[�I�u�W�F�N�g���X�|�i�[�̎q�ɐݒ�
            obj.transform.SetParent(transform);

            // �v���C���[�R���|�[�l���g���擾
            _currentPlayer = obj.GetComponent<Player>();

            // �v���C���[���X�g��0�Ԗڂ�j��
            _playerList.RemoveAt(0);

            _isPlayerCount = false;
        }
    }

    private void GetPlayerSpawn()
    {
        Invoke(nameof(PlayerSpawn), 2.7f);
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
}
