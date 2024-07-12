using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /// <summary>プレイヤーPrefab</summary>
    [SerializeField] Player[] _playerPrefabs;

    /// <summary>Player出現カウント</summary>
    int _playercount = 0;

    /// <summary>Player出現真偽</summary>
    public bool IsPlayerCount = true;

    /// <summary>フィールド上にいるEnemyの数
    [SerializeField] GameObject[] enemyBox;

    /// <summary>スコアテキスト</summary>
    [SerializeField] Text ScoreText;

    /// <summary>スコア</summary>
    int _score;

    /// <summary>Enemyの残機テキスト</summary>
    [SerializeField] Text EnemyText;

    /// <summary>Enemyの残機</summary>
    int _enemyScore;

    /// <summary>Finish真偽</summary>
    private bool isFinish = false;

    /// <summary>終わりボタン</summary>
    [SerializeField] GameObject FinishButtom;

    /// <summary>GameOver真偽</summary>
    private bool isGameOver = false;

    /// <summary>GameOverボタン</summary>
    [SerializeField] GameObject GameOverButton;

    /// <summary>持っているアイテムのリスト</summary>
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
        //Enemyのカウントが0になったら実行
        GameClear();
        //Playerのスポーン
        PlayerSpawn();

        GameOver();

        // アイテムを使う
        if (Input.GetKeyDown(KeyCode.Space))
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

    public void Count()
    {
        if (IsPlayerCount = true)
        {
            _playercount++;
        }
    }

    /// <summary>Playerのスポーン</summary>
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

    /// <summary>Scoreを加算</summary>
    public void AddScore()
    {
        _score += 500;
        ScoreText.text = "SCORE:" + _score;
    }

    /// <summary>Enemyの数</summary>
    public void EnemyScore()
    {
        _enemyScore += 1;
        EnemyText.text = "ENEMY:" + _enemyScore + "/" + enemyBox.Length;
        /// <summary>スコアテキストがEnemyの数を上回ったら</summary>
        if (_enemyScore >= enemyBox.Length)
        {
            isFinish = true;
        }
    }

    /// <summary>FinishButtomを表示</summary>
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

    /// <summary>リザルト画面へロード</summary>
    void Result()
    {
        SceneManager.LoadScene("Result");
    }
        
    /// <summary>0.5秒後に作動</summary>
    public void GetStage1()
    {
        Invoke(nameof(Result), 0.5f);
    }
}
