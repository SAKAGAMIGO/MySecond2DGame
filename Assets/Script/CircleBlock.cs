using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    private int speed;                //�I�u�W�F�N�g�̃X�s�[�h
    private int radius;               //�~��`�����a
    private Vector2 defPosition;      //defPosition��Vector3�Œ�`����B
    float x;
    float y;

    // Use this for initialization
    void Start()
    {
        speed = 1;
        radius = 2;

        defPosition = transform.position;    //defPosition�������̂���ʒu�ɐݒ肷��B
    }

    // Update is called once per frame
    void Update()
    {
        x = radius * Mathf.Sin(Time.time * speed);      //X���̐ݒ�
        y = radius * Mathf.Cos(Time.time * speed);      //Z���̐ݒ�

        transform.position = new Vector2(x + defPosition.x, defPosition.y );  //�����̂���ʒu������W�𓮂����B



    }
}


