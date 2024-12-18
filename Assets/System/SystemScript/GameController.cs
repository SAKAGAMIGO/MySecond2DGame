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
        // 最初のプレイヤーをスポーンする
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
        //Enemyのカウントが0になったら実行
        GameClear();

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

        //TNTプレイヤーを０番目に出現
        _playerList.Insert(0, _tntBullet);

        // 通常プレイヤーもリストに格納しておく
        if (_playerList.Count == 1) // 最初のTNTプレイヤー追加時のみ
        {
            _playerList.Add(_currentPlayerPrefab);  // 通常プレイヤーを追加
        }
    }


    /// <summary>Playerのスポーン</summary>
    private void PlayerSpawn()
    {
        if (_playerList.Count > 0)
        {
            // リストからプレイヤーを取得
            Player newPlayer = _playerList[0];
            _playerList.RemoveAt(0);

            // スポナーの位置に生成
            GameObject obj = Instantiate(newPlayer.gameObject, transform.position, Quaternion.identity);

            // プレイヤーをスポナーの子として設定
            obj.transform.SetParent(transform);

            // プレイヤーにGameControllerを登録
            Player spawnedPlayer = obj.GetComponent<Player>();
            spawnedPlayer.Initialize(this);

            // 現在のプレイヤーとして保持
            _currentPlayer = spawnedPlayer;
        }
    }

    /// <summary>プレイヤーが破壊されたときの処理</summary>
    public void OnPlayerDestroyed()
    {
        if (_playerList.Count == 0)
        {
            // すべてのプレイヤーが破壊されたらゲームオーバー
            GameOver();
        }
        else
        {
            // 次のプレイヤーをスポーン
            Invoke(nameof(PlayerSpawn), 2.7f);
            Debug.Log("PlayerSpawn");
        }
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
