using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SceneChenge;
using static UnityEditor.Progress;

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

    /// <summary>�L�[��ItemType, �l��Int</summary>
    public static Dictionary<ItemType, int> _itemCount = new Dictionary<ItemType, int>();

    [SerializeField] GameObject _pawerItemButton;
    [SerializeField] GameObject _tntButtom;
    [SerializeField] GameObject _sightButtom;

    [SerializeField] TNTItem _tntItem;
    [SerializeField] SightItem _sightItem;
    [SerializeField] ItemAddForce _itemAddForce;

    /// <summary></summary>
    [SerializeField] Vector2 _taregetPos;
    /// <summary>GameController��_taregetPos�^�U</summary>
    public bool _isTareget;

    bool _isCurrentPlayerMove = false;

    private static bool _isFirstEntry = true;

    Animator _animator;

    public void Start()
    {
        // �ŏ��̃v���C���[���X�|�[������
        Invoke(nameof(PlayerSpawn), 2.7f);

        _enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        _animator = GameObject.Find("Man_Gun").GetComponent<Animator>();
        _enemyText.text = "ENEMY:" + _enemyScore + "/" + _enemyBox.Length;
        _finishButtom.SetActive(false);
        _gameOverButton.SetActive(false);

        if (_isFirstEntry)
        {
            if (!_itemCount.ContainsKey(ItemType.TNT))
                _itemCount.Add(ItemType.TNT, 3);

            if (!_itemCount.ContainsKey(ItemType.Fly))
                _itemCount.Add(ItemType.Fly, 3);
        }
    }

    private void Update()
    {
        //Enemy�̃J�E���g��0�ɂȂ�������s
        GameClear();

        if (_enemyElementCount <= _enemyScore)
        {
            _isTareget = true;
            transform.position = _taregetPos;
        }
    }

    // �A�C�e�����g��
    public void UseItem(ItemType itemType)
    {
        // ��������܂��̓A�C�e�������݂��Ȃ��ꍇ�͉������Ȃ�
        if (_itemCount.Count == 0 || !_itemCount.ContainsKey(itemType))
        {
            Debug.Log("�A�C�e�������݂��܂���B");
            return;
        }

        ItemBaceClass item = null;
        if (itemType == ItemType.TNT)
        {
            // item�� _itemDic[itemType] ���i�[
            var tnt = Instantiate(_tntItem);
            tnt.AddController(this);
            item = tnt;
        }
        else if (itemType == ItemType.Fly)
        {
            var fly = Instantiate(_itemAddForce);
            fly.AddPlayer(_currentPlayer);
            item = fly;
        }

        // �A�C�e���̃A�N�e�B�u��
        item.Activate();
        //_itemCount�����炷
        _itemCount[itemType]--;

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
        Debug.Log("AddTNT");

        //TNT�v���C���[���O�Ԗڂɏo��
        _playerList.Insert(0, _tntBullet);

        // �ʏ�v���C���[�����X�g�Ɋi�[���Ă���
        if (_playerList.Count == 1) // �ŏ���TNT�v���C���[�ǉ����̂�
        {
            _playerList.Add(_currentPlayerPrefab);  // �ʏ�v���C���[��ǉ�
        }
    }


    /// <summary>Player�̃X�|�[��</summary>
    private void PlayerSpawn()
    {
        if (_playerList.Count > 0)
        {
            // ���X�g����v���C���[���擾
            Player newPlayer = _playerList[0];
            _playerList.RemoveAt(0);

            // �X�|�i�[�̈ʒu�ɐ���
            GameObject obj = Instantiate(newPlayer.gameObject, transform.position, Quaternion.identity);

            // �v���C���[���X�|�i�[�̎q�Ƃ��Đݒ�
            obj.transform.SetParent(transform);

            // �v���C���[��GameController��o�^
            Player spawnedPlayer = obj.GetComponent<Player>();
            spawnedPlayer.Initialize(this);

            // ���݂̃v���C���[�Ƃ��ĕێ�
            _currentPlayer = spawnedPlayer;
        }
    }

    /// <summary>�v���C���[���j�󂳂ꂽ�Ƃ��̏���</summary>
    public void OnPlayerDestroyed()
    {
        if (_playerList.Count == 0)
        {
            // ���ׂẴv���C���[���j�󂳂ꂽ��Q�[���I�[�o�[
            GameOver();
        }
        else
        {
            // ���̃v���C���[���X�|�[��
            Invoke(nameof(PlayerSpawn), 2.7f);
            Debug.Log("PlayerSpawn");
        }
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
        if (_isFinish && !_isGameOver)
        {
            _finishButtom.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (!_isGameOver && !_isFinish)
        {
            _isGameOver = true;
            _animator.Play("Death");
            _gameOverButton.SetActive(true);
        }
    }
}
