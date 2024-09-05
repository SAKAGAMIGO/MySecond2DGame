using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private Vector3 targetPosition; // 目標位置
    private bool isMoving = false; // 移動中フラグ
    Animator _animator;

    private void Start()
    {
        _animator = GameObject.Find("Man").GetComponent<Animator>();
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

        if (isMoving == false)
        {
            _animator.SetBool("Wolk", false);
        }
    }

    // 目標位置をセットし移動を開始する
    public void MoveTo(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }
}
