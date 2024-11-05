using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public GameObject movingObject; // �ړ�������I�u�W�F�N�g
    public Button[] stageButtons; // �X�e�[�W�{�^��
    public float moveSpeed = 2f; // �ړ����x

    private Vector3 targetPosition; // �ڕW�ʒu

    Animator _animator;

    private void Start()
    {
        // �{�^���Ƀ��X�i�[��ǉ�
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int index = i; // �N���[�W���[�̂��߂̃C���f�b�N�X�R�s�[
            stageButtons[i].onClick.AddListener(() => OnStageButtonClicked(index));
        }

        // PlayerPrefs����ۑ����ꂽ�ʒu��ǂݍ���
        LoadTargetPosition();
        movingObject.transform.position = targetPosition; // �I�u�W�F�N�g�̏����ʒu��ݒ�

        _animator = GameObject.Find("Man").GetComponent<Animator>();
    }

    private void Update()
    {
        // �ړ�����
        if (movingObject.transform.position != targetPosition)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnStageButtonClicked(int index)
    {
        // �{�^���ɉ������ʒu��ݒ�
        targetPosition = stageButtons[index].transform.position;

        // �ڕW�ʒu��PlayerPrefs�ɕۑ�
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
            // �����ʒu��ݒ�i�K�v�ɉ����āj
            targetPosition = movingObject.transform.position; // �܂��̓f�t�H���g�̈ʒu��ݒ�
        }
    }
}
