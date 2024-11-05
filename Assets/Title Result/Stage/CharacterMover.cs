using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private Vector3 targetPosition; // 目標位置
    private bool isMoving = false; // 移動中フラグ
    Animator _animator;

    // static変数で目標位置を保持
    public static Vector3 StaticTargetPosition;

    private void Start()
    {
        _animator = GameObject.Find("Man").GetComponent<Animator>();

        // static変数から目標位置を読み込む
        LoadTargetPosition();
    }

    void Update()
    {
        if (isMoving)
        {
            _animator.SetBool("Wolk", true);
            // キャラクターを目標位置に向かって移動
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 目標位置に到達したら移動を止める
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }

        if (!isMoving)
        {
            _animator.SetBool("Wolk", false);
        }
    }

    // 目標位置をセットし移動を開始する
    public void MoveTo(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;

        // 目標位置をstatic変数に保存
        SaveTargetPosition();
    }

    private void SaveTargetPosition()
    {
        // static変数に目標位置を保存
        StaticTargetPosition = targetPosition;
    }

    private void LoadTargetPosition()
    {
        if (PlayerPrefs.HasKey("TargetPositionX") && PlayerPrefs.HasKey("TargetPositionY") && PlayerPrefs.HasKey("TargetPositionZ"))
        {
            float x = PlayerPrefs.GetFloat("TargetPositionX");
            float y = PlayerPrefs.GetFloat("TargetPositionY");
            float z = PlayerPrefs.GetFloat("TargetPositionZ");
            targetPosition = new Vector3(x, y, z);

            Debug.Log($"読み込まれた目標位置: {targetPosition}");
        }
        else
        {
            // 初期位置を設定
            targetPosition = transform.position;
            Debug.Log("目標位置が存在しないため、初期位置を使用します: " + targetPosition);
        }
    }
}
