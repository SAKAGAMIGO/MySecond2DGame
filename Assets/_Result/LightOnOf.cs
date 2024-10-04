using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOf : MonoBehaviour
{
    // 操作対象のゲームオブジェクトのリスト
    public GameObject[] gameObjects;

    void Start()
    {
        // 1秒ごとにランダムなオブジェクトをアクティブ/非アクティブにする
        InvokeRepeating("RandomActivate", 1f, 1f);
    }

    void RandomActivate()
    {
        // ランダムに1つのオブジェクトを選ぶ
        int randomIndex = Random.Range(0, gameObjects.Length);

        // 選んだオブジェクトのアクティブ状態を切り替える
        bool isActive = gameObjects[randomIndex].activeSelf;
        gameObjects[randomIndex].SetActive(!isActive);
    }
}
