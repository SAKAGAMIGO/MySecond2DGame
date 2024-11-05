using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public GameObject movingObject; // 移動させるオブジェクト
    public Button[] stageButtons; // ステージボタン
    public float moveSpeed = 2f; // 移動速度

    private Vector3 targetPosition; // 目標位置

    Animator _animator;

    private void Start()
    {
        // ボタンにリスナーを追加
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int index = i; // クロージャーのためのインデックスコピー
            stageButtons[i].onClick.AddListener(() => OnStageButtonClicked(index));
        }

        // PlayerPrefsから保存された位置を読み込む
        LoadTargetPosition();
        movingObject.transform.position = targetPosition; // オブジェクトの初期位置を設定

        _animator = GameObject.Find("Man").GetComponent<Animator>();
    }

    private void Update()
    {
        // 移動処理
        if (movingObject.transform.position != targetPosition)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnStageButtonClicked(int index)
    {
        // ボタンに応じた位置を設定
        targetPosition = stageButtons[index].transform.position;

        // 目標位置をPlayerPrefsに保存
        SaveTargetPosition();

        _animator.SetBool("Wolk", true);
    }

    private void SaveTargetPosition()
    {
        PlayerPrefs.SetFloat("TargetPositionX", targetPosition.x);
        PlayerPrefs.SetFloat("TargetPositionY", targetPosition.y);
        PlayerPrefs.SetFloat("TargetPositionZ", targetPosition.z);
        PlayerPrefs.Save();
    }

    private void LoadTargetPosition()
    {
        if (PlayerPrefs.HasKey("TargetPositionX") && PlayerPrefs.HasKey("TargetPositionY") && PlayerPrefs.HasKey("TargetPositionZ"))
        {
            float x = PlayerPrefs.GetFloat("TargetPositionX");
            float y = PlayerPrefs.GetFloat("TargetPositionY");
            float z = PlayerPrefs.GetFloat("TargetPositionZ");
            targetPosition = new Vector3(x, y, z);
        }
        else
        {
            // 初期位置を設定（必要に応じて）
            targetPosition = movingObject.transform.position; // またはデフォルトの位置を設定
        }
    }
}
