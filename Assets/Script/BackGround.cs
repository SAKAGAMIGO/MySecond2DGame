using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField] GameObject Player;

    //初期位置
    [SerializeField] Vector3 lastPlayerPos;

    //移動の速度
    [SerializeField] float _moveSpeed;


    void Start()
    {
        lastPlayerPos = Player.transform.position;
    }

    
    void Update()
    {
        //今の位置から初期位置を引き移動速度をかけるY軸は０
        transform.Translate(new Vector2((Player.transform.position.x - lastPlayerPos.x) * _moveSpeed,0));

        lastPlayerPos = Player.transform.position;
    }
}
