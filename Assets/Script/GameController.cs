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

    /// <summary>Player出現カウント</summary>
    int _playercount = 0;

    /// <summary>Player出現真偽</summary>
    public bool IsPlayerCount = true;

    /// <summary>フィールド上にいるEnemyの数
    [SerializeField] GameObject[] _enemyBox;

    /// <summary>Enemyの残機テキスト</summary>
    [SerializeField] Text _enemyText;

    /// <summary>Enemyの残機</summary>
    int _enemyScore;

    /// <summary>Finish真偽</summary>
    private bool _isFinish = false;

    /// <summary>終わりボタン</summary>
    [SerializeField] GameObject _finishButtom;

    /// <summary>GameOver真偽</summary>
    private bool _isGameOver = false;

    /// <summary>GameOverボタン</summary>
    [SerializeField] GameObject _gameOverButton;

    /// <summary>Zoomボタン</summary>
    [SerializeField] GameObject _zoomButton;
    [SerializeField] GameObject _outButton;

    /// <summary>Zoom,Out真偽</summary>
    bool _isZoom;
    bool _isOut;

    /// <summary>VacualCamera</summary>
    [SerializeField] CinemachineVirtualCamera _vCamera;

    /// <summary>プレイヤーPrefab</summary>
    [SerializeField] List<Player> _playerList = new List<Player>();

    /// <summary>持っているアイテムのリスト</summary>
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
        //Enemyのカウントが0になったら実行
        GameClear();

        //Playerのスポーン
        GetPlayerSpawn();

        GameOver();

        Zoom();
        Out();
    }

    /// <summary>
    /// アイテムをアイテムリストに追加する
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBaceClass item)
    {
        _itemList.Add(item);
        Debug.Log(item);
    }

    // アイテムを使う
    public void UseItem()
    {
        if (_itemList.Count > 0)
        {
            // リストの先頭にあるアイテムを使って、破棄する
            ItemBaceClass item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
            Debug.Log("アイテム使用");
        }
    }
    
    public void UseTNT()
    {
        if (_itemList.Count > 0)
        {
            // リストの先頭にあるアイテムを使って、破棄する
            ItemBaceClass item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
            Debug.Log("アイテム使用");
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

    /// <summary>Playerのスポーン</summary>
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

    //Zoomボタンアクティブ管理
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

    //Outボタンアクティブ管理
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

    //VCameraIdolの優先度変更
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

    /// <summary>リザルト画面へロード</summary>
    void Result()
    {
        SceneManager.LoadScene("Result");
    }

    /// <summary>0.5秒後に作動</summary>
    public void GetResult()
    {
        Invoke(nameof(Result), 2f);
    }
}
