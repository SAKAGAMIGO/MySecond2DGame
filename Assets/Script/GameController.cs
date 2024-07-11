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

    /// <summary>スコアテキスト</summary>
    [SerializeField] Text ScoreText;

    /// <summary>Enemyの残機テキスト</summary>
    [SerializeField] Text EnemyText;

    /// <summary>スコア</summary>
    int _score;

    /// <summary>Enemyの残機</summary>
    int _enemyScore;

    /// <summary>フィールド上にいるEnemyの数
    public GameObject[] enemyBox;

    /// <summary>終わりボタン</summary>
    [SerializeField] GameObject FinishButtom;

    /// <summary>GameOverボタン</summary>
    [SerializeField] GameObject GameOverButton;

    /// <summary>Player出現カウント</summary>
    int count = 0;

    /// <summary>Player出現真偽</summary>
    public bool isCount = true;

    /// <summary>GameOver真偽</summary>
    public bool isGameOver = false;

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
    }

    public void Count()
    {
        if (isCount = true)
        {
            count++;
        }
    }

    /// <summary>Playerのスポーン</summary>
    private void PlayerSpawn()
    {
        if (_playerPrefabs.Length > count)
        {
            if (isCount == true)
            {
                Instantiate(_playerPrefabs[count], transform.position, Quaternion.identity);
                isCount = false;
                //count = (count + 1) % _playerPrefabs.Length;
                
                
            }
        }
        else if (_playerPrefabs.Length <= count)
        {
            isGameOver = true;
            Debug.LogWarning("GameOver");
            Debug.LogWarning(_playerPrefabs.Length + "to" + count);
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
    }

    /// <summary>FinishButtomを表示</summary>
    private void GameClear()
    {
        if (_enemyScore >= enemyBox.Length)
        {
            FinishButtom.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (isGameOver)
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
