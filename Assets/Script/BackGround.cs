using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField] GameObject Player;

    //�����ʒu
    [SerializeField] Vector3 lastPlayerPos;

    //�ړ��̑��x
    [SerializeField] float _moveSpeed;


    void Start()
    {
        lastPlayerPos = Player.transform.position;
    }

    
    void Update()
    {
        //���̈ʒu���珉���ʒu�������ړ����x��������Y���͂O
        transform.Translate(new Vector2((Player.transform.position.x - lastPlayerPos.x) * _moveSpeed,0));

        lastPlayerPos = Player.transform.position;
    }
}
