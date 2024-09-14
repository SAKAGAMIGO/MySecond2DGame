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

    /// <summary>キーがItemType, 値がItemBaceClass</summary>
    [SerializeField] Dictionary<ItemType, ItemBaceClass> _itemDic = new Dictionary<ItemType, ItemBaceClass>();
    /// <summary>キーがItemType, 値がInt</summary>
    public Dictionary<ItemType, int> _itemCount = new Dictionary<ItemType, int>();

    [SerializeField] GameObject _pawerItemButton;
    [SerializeField] GameObject _tntButtom;
    [SerializeField] GameObject _sightButtom;

    /// <summary></summary>
    [SerializeField] Vector2 _taregetPos;
    /// <summary>GameControllerの_taregetPos真偽</summary>
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

    /// <summary>
    /// アイテムをアイテムリストに追加する
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemType itemType, ItemBaceClass item)
    {
        //_itemDicがitemTypeだったら(ContainsKey = bool型)
        if (_itemDic.ContainsKey(itemType))
        {
            _itemCount[itemType]++;
            Debug.Log("アイテム取得" + _itemCount + item);
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

    // アイテムを使う
    public void UseItem(ItemType itemType)
    {
        //if (_itemDic.Count > 0)
        //{
        //    if (_itemDic[itemType] == null || _itemCount.Count > 0)
        //    {
        //        return;
        //    }

        //    // itemに _itemDicを格納
        //    ItemBaceClass item = _itemDic[itemType];

        //    item.Activate();
        //}
        //_itemCount[itemType]--;
        //Debug.Log("アイテム使用" + _itemCount);

        // 辞書が空またはアイテムが存在しない場合は何もしない
        if (_itemDic.Count == 0 || !_itemDic.ContainsKey(itemType))
        {
            Debug.Log("アイテムが存在しません。");
            return;
        }

        // 指定したアイテムがnullまたはカウントが0以下の場合は何もしない
        if (_itemDic[itemType] == null || !_itemCount.ContainsKey(itemType) || _itemCount[itemType] <= 0)
        {
            Debug.Log("アイテムが存在しないか、カウントが0です。");
            return;
        }

        // itemに _itemDic[itemType] を格納
        ItemBaceClass item = _itemDic[itemType];

        // アイテムのアクティブ化
        item.Activate();

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
            _gameOverButton.SetActive(true);
        }
    }
}
