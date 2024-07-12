using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    private Vector3 _diff; //カメラとプレイヤーの距離
    private GameObject _target; //追従するターゲットオブジェクト
    public float followSpeed; //追従するスピード

    //ズームイン、ズームアウト判定
    bool _isZoom;
    bool _isOut;

    [SerializeField] GameObject _zoomText;
    [SerializeField] GameObject _outText;

    void Start()
    {
        _outText.SetActive(false);
        _isZoom = true; //ズームイン可能にする
        _isOut = false; //ズームアウト不可能にする
    }

    public void Follow()
    {
        
        if (_isZoom = true)
        {
            Debug.Log("フォロー");
            _target = GameObject.Find("CameraTarget");//名前がPlayerのオブジェクトを取得してターゲットに指定
            _diff = _target.transform.position - this.transform.position; //カメラとプレイヤーの初期の距離を指定
            transform.position = Vector3.Lerp(this.transform.position, _target.transform.position - _diff, Time.deltaTime * followSpeed); //線形補間関数によるカメラの移動
            _isOut = true;
        }
    }

    public void Aut()
    {
        if (_isOut = true)
        {
            _isZoom = true;

        }
    }

    private void Update()
    {
        //Iキーが押されているかつズームインが可能なとき
        if (Input.GetKey(KeyCode.I) && _isZoom == true)
        {
            Follow();
            _isZoom = false;
            _zoomText.SetActive(false);
            _outText.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.O) && _isOut == true)
        {
            _outText.SetActive(false);
            _zoomText.SetActive(true);
        }
    }
}
