using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField] Camera _camera;

    //�����ʒu
    [SerializeField] Vector3 lastPlayerPos;

    //�ړ��̑��x
    [SerializeField] float _moveSpeed;


    void Start()
    {
        lastPlayerPos = _camera.transform.position;
    }

    void Update()
    {
        //���̈ʒu���珉���ʒu�������ړ����x��������Y���͂O
        transform.Translate(new Vector2((_camera.transform.position.x - lastPlayerPos.x) * _moveSpeed, 0));

        lastPlayerPos = _camera.transform.position;
    }
}
