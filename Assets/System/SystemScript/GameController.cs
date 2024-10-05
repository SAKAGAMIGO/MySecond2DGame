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

    /// <summary>Player出現カウント</summary>
    int _playercount = 0;

    /// <summary>Player出現真偽</summary>
    public bool _isPlayerCount = true;

    /// <summary>Enemyの残機テキスト</summary>
    [SerializeField] Text _enemyText;

    /// <summary>Enemyの残機</summary>
    public int _enemyScore;

    /// <summary>Finish真偽</summary>
    public bool _isFinish = false;

    /// <summary>終わりボタン</summary>
    [SerializeField] GameObject _finishButtom;

    /// <summary>GameOver真偽</summary>
    public bool _isGameOver = false;

    /// <summary>GameOverボタン</summary>
    [SerializeField] GameObject _gameOverButton;

    /// <summary>フィールド上にいるEnemyの数</summary>
    [SerializeField] GameObject[] _enemyBox;

    //<summary>現在のEnemyの数</summary>
    public int _enemyElementCount;

    /// <summary>プレイヤーPrefab</summary>
    [SerializeField] List<Player> _playerList = new List<Player>();

    //現在出現していプレイヤーのプレファブ
    [SerializeField] Player _currentPlayerPrefab;

    //現在のプレイヤー
    [SerializeField] Player _currentPlayer;

    /// <summary>キーがItemType, 値がInt</summary>
    public static Dictionary<ItemType, int> _itemCount = new Dictionary<ItemType, int>();

    [SerializeField] GameObject _pawerItemButton;
    [SerializeField] GameObject _tntButtom;
    [SerializeField] GameObject _sightButtom;

    [SerializeField] TNTItem _tntItem;
    [SerializeField] SightItem _sightItem;
    [SerializeField] ItemAddForce _itemAddForce;

    /// <summary></summary>
    [SerializeField] Vector2 _taregetPos;
    /// <summary>GameControllerの_taregetPos真偽</summary>
    public bool _isTareget;

    bool _isCurrentPlayerMove = false;

    private static bool _isFirstEntry = true;

    Animator _animator;

    public void Start()
    {
        _enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        _animator = GameObject.Find("Man_Gun").GetComponent<Animator>();
        _enemyText.text = "ENEMY:" + _enemyScore + "/" + _enemyBox.Length;
        _finishButtom.SetActive(false);
        _gameOverButton.SetActive(false);

        if (_isFirstEntry)
        {
            _itemCount.Add(ItemType.TNT, 3);
            _itemCount.Add(ItemType.Fly, 3);

        }
    }

    private void Update()
    {
        //Enemyのカウントが0になったら実行
        GameClear();

        if (_isPlayerCount == true)
        {
            //Playerのスポーン
            GetPlayerSpawn();
        }

        GameOver();

        if (_enemyElementCount <= _enemyScore)
        {
            _isTareget = true;
            transform.position = _taregetPos;
        }
    }

    // アイテムを使う
    public void UseItem(ItemType itemType)
    {
        // 辞書が空またはアイテムが存在しない場合は何もしない
        if (_itemCount.Count == 0 || !_itemCount.ContainsKey(itemType))
        {
            Debug.Log("アイテムが存在しません。");
            return;
        }

        ItemBaceClass item = null;
        if (itemType == ItemType.TNT)
        {
            // itemに _itemDic[itemType] を格納
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

        // アイテムのアクティブ化
        item.Activate();
        //_itemCountを減らす
        _itemCount[itemType]--;

        // アイテムカウントが0になったら辞書から削除する（必要に応じて）
        if (_itemCount[itemType] == 0)
        {
            _itemCount.Remove(itemType);
            Debug.Log($"アイテム {itemType} のカウントが0になったので削除しました。");
        }
        else
        {
            Debug.Log($"アイテム {itemType} を使用しました。残りカウント: {_itemCount[itemType]}");
        }
    }

    public void AddTNT()
    {
        Debug.Log("AddTNT");
        //現在出現していプレイヤーのプレファブを０番目に出現
        _playerList.Insert(0, _currentPlayerPrefab);
        //TNTプレイヤーを０番目に出現
        _playerList.Insert(0, _tntBullet);
        //現在のプレイヤーを破棄
        Destroy(_currentPlayer.gameObject);
    }


    /// <summary>Playerのスポーン</summary>
    private void PlayerSpawn()
    {
        if (_isPlayerCount == true)
        {
            // 現在出現しているプレイヤーのプレファブにプレイヤーリストの0番目を格納
            _currentPlayerPrefab = _playerList[0];

            // プレイヤーオブジェクトをスポナーの位置に生成
            GameObject obj = Instantiate(_playerList[0].gameObject, transform.position, Quaternion.identity);

            // プレイヤーオブジェクトをスポナーの子に設定
            obj.transform.SetParent(transform);

            // プレイヤーコンポーネントを取得
            _currentPlayer = obj.GetComponent<Player>();

            // プレイヤーリストの0番目を破棄
            _playerList.RemoveAt(0);

            _isPlayerCount = false;
        }
    }

    private void GetPlayerSpawn()
    {
        Invoke(nameof(PlayerSpawn), 2.7f);
    }

    /// <summary>Enemyの数</summary>
    public void EnemyScore()
    {
        _enemyScore += 1;
        _enemyText.text = "ENEMY:" + _enemyScore + "/" + _enemyBox.Length;
        /// <summary>スコアテキストがEnemyの数を上回ったら</summary>
        if (_enemyScore >= _enemyBox.Length)
        {
            _isFinish = true;
        }
    }

    /// <summary>FinishButtomを表示</summary>
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
            _animator.Play("Death");
            _gameOverButton.SetActive(true);
        }
    }
}
