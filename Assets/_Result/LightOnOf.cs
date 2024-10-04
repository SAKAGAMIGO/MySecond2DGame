using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOf : MonoBehaviour
{
    // ����Ώۂ̃Q�[���I�u�W�F�N�g�̃��X�g
    public GameObject[] gameObjects;

    void Start()
    {
        // 1�b���ƂɃ����_���ȃI�u�W�F�N�g���A�N�e�B�u/��A�N�e�B�u�ɂ���
        InvokeRepeating("RandomActivate", 1f, 1f);
    }

    void RandomActivate()
    {
        // �����_����1�̃I�u�W�F�N�g��I��
        int randomIndex = Random.Range(0, gameObjects.Length);

        // �I�񂾃I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�؂�ւ���
        bool isActive = gameObjects[randomIndex].activeSelf;
        gameObjects[randomIndex].SetActive(!isActive);
    }
}
