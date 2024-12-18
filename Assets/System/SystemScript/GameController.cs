using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static SceneChenge;
using static UnityEditor.Progress;

public class GameController : MonoBehaviour
{
    [SerializeField, Tooltip("�v���C���[Prefab")]
    List<Player> _playerList = new List<Player>();
    [SerializeField, Tooltip("���ݏo�����Ă��v���C���[�̃v���t�@�u")]
    Player _currentPlayerPrefab;
    [SerializeField, Tooltip("TNT�o���b�g")]
    Player _tntBullet;
    [SerializeField, Tooltip("���݂̃v���C���[")]
    Player _currentPlayer;
    [SerializeField, Tooltip("�t�B�[���h��ɂ���Enemy�̐�")]
    GameObject[] _enemyBox;

    [SerializeField, Tooltip("�ړ��ʒu")]
    Vector2 _taregetPos;

    [SerializeField, Tooltip("Enemy�̎c�@�e�L�X�g")]
    Text _enemyText;
    [SerializeField, Tooltip("�I���{�^��")]
    GameObject _finishButtom;
    [SerializeField, Tooltip("GameOver�{�^��")]
    GameObject _gameOverButton;
    [SerializeField, Tooltip("TNT�A�C�e������\��")]
    TextMeshProUGUI _tntText;
    [SerializeField, Tooltip("Fly�A�C�e������\��")]
    TextMeshProUGUI _flyText;
    [SerializeField, Tooltip("�^�C�g���V�[����")]
    string _titleSceneName = "Title";

    [SerializeField, Tooltip("�G�i�W�[�h�����N")]
    GameObject _pawerItemButton;
    [SerializeField, Tooltip("TNT")]
    GameObject _tntButtom;
    [SerializeField, Tooltip("TNT�̃C���X�^���X")]
    TNTItem _tntItem;
    [SerializeField, Tooltip("�G�i�W�[�h�����N�̃C���X�^���X")]
    ItemAddForce _itemAddForce;

    /// <summary>�L�[��ItemType, �l��Int</summary>
    public static Dictionary<ItemType, int> _itemCount = new Dictionary<ItemType, int>();

    /// <summary>Player�o���J�E���g</summary>
    int _playercount = 0;
    //<summary>���݂�Enemy�̐�</summary>
    public int _enemyElementCount;

    bool _isTNT = false;
    /// <summary>Player�o���^�U</summary>
    public bool _isPlayerCount = true;
    /// <summary>Enemy�̎c�@</summary>
    public int _enemyScore;
    /// <summary>Finish�^�U</summary>
    public bool _isFinish = false;
    /// <summary>GameOver�^�U</summary>
    public bool _isGameOver = false;
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
                _itemCount.Add(ItemType.TNT, 5);

            if (!_itemCount.ContainsKey(ItemType.Fly))
                _itemCount.Add(ItemType.Fly, 5);
        }

        // �ŏ��̃e�L�X�g�X�V
        UpdateItemText();
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

    private void UpdateItemText()
    {
        if (_itemCount.ContainsKey(ItemType.TNT))
            _tntText.text = $"{_itemCount[ItemType.TNT]}";
        else
            _tntText.text = "0";

        if (_itemCount.ContainsKey(ItemType.Fly))
            _flyText.text = $"{_itemCount[ItemType.Fly]}";
        else
            _flyText.text = "0";
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
        else if (itemType == ItemType.Fly && _currentPlayer != null)
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

        UpdateItemText(); // �g�p��Ƀe�L�X�g�X�V
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

    public void OnPlayerCollision()
    {
        if (_currentPlayer != null)
        {
            Debug.Log($"�v���C���[ {_currentPlayer.name} ���R���W�������܂����B���݂̃v���C���[�����Z�b�g���܂��B");
            _currentPlayer = null;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _titleSceneName)
        {
            RefillItems();
        }
    }

    private void RefillItems()
    {
        if (!_itemCount.ContainsKey(ItemType.TNT))
            _itemCount[ItemType.TNT] = 3;
        else
            _itemCount[ItemType.TNT] += 3;

        if (!_itemCount.ContainsKey(ItemType.Fly))
            _itemCount[ItemType.Fly] = 3;
        else
            _itemCount[ItemType.Fly] += 3;

        UpdateItemText();
    }

}
