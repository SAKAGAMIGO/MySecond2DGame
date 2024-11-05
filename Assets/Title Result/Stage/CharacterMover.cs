using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float moveSpeed = 5f; // �ړ����x
    private Vector3 targetPosition; // �ڕW�ʒu
    private bool isMoving = false; // �ړ����t���O
    Animator _animator;

    // static�ϐ��ŖڕW�ʒu��ێ�
    public static Vector3 StaticTargetPosition;

    private void Start()
    {
        _animator = GameObject.Find("Man").GetComponent<Animator>();

        // static�ϐ�����ڕW�ʒu��ǂݍ���
        LoadTargetPosition();
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

        if (!isMoving)
        {
            _animator.SetBool("Wolk", false);
        }
    }

    // �ڕW�ʒu���Z�b�g���ړ����J�n����
    public void MoveTo(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;

        // �ڕW�ʒu��static�ϐ��ɕۑ�
        SaveTargetPosition();
    }

    private void SaveTargetPosition()
    {
        // static�ϐ��ɖڕW�ʒu��ۑ�
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

            Debug.Log($"�ǂݍ��܂ꂽ�ڕW�ʒu: {targetPosition}");
        }
        else
        {
            // �����ʒu��ݒ�
            targetPosition = transform.position;
            Debug.Log("�ڕW�ʒu�����݂��Ȃ����߁A�����ʒu���g�p���܂�: " + targetPosition);
        }
    }
}
