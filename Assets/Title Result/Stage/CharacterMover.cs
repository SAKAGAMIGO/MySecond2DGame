using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float moveSpeed = 5f; // �ړ����x
    private Vector3 targetPosition; // �ڕW�ʒu
    private bool isMoving = false; // �ړ����t���O
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
            // �L�����N�^�[��ڕW�ʒu�Ɍ������Ĉړ�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �ڕW�ʒu�ɓ��B������ړ����~�߂�
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

    // �ڕW�ʒu���Z�b�g���ړ����J�n����
    public void MoveTo(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }
}
