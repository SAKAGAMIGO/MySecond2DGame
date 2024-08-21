using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>Zoomボタン</summary>
    [SerializeField] GameObject _zoomButton;
    [SerializeField] GameObject _outButton;

    /// <summary>Zoom,Out真偽</summary>
    bool _isZoom;
    bool _isOut;

    /// <summary>VacualCamera</summary>
    [SerializeField] CinemachineVirtualCamera _zoomCamera;
    [SerializeField] CinemachineVirtualCamera _camera1;
    [SerializeField] CinemachineVirtualCamera _camera2;

    GameController _controller;

    private Animator _animator;

    public void Start()
    {
        _zoomButton.SetActive(true);
        _outButton.SetActive(false);
        _isZoom = true;
        _isOut = false;
        _controller = GameObject.FindObjectOfType<GameController>();
        //PlayerMotionのアニメーションを格納
        _animator = GameObject.Find("GameController").GetComponent<Animator>();
    }

    public void Update()
    {
        Zoom();
        Out();

        if (_controller._enemyElementCount <= _controller._enemyScore)
        {
            _camera1.Priority = 0;
            _camera2.Priority = 30;
            _zoomCamera.Priority = 0;
            _animator.SetBool("Move", true);
        }
    }

        //Zoomボタンアクティブ管理
    public void Zoom()
    {
        if (_isZoom && _isOut == false)
        {
            _zoomButton.SetActive(true);
        }
        else
        {
            _zoomButton.SetActive(false);
        }
    }

    //Outボタンアクティブ管理
    public void Out()
    {
        if (_isOut && _isZoom == false)
        {
            _outButton.SetActive(true);
        }
        else
        {
            _outButton.SetActive(false);
        }
    }

    //VCameraIdolの優先度変更
    public void ZoomCamera()
    {
        _zoomCamera.Priority = 0;
        _isZoom = false;
        _isOut = true;
        Debug.Log("推された");
    }
    public void OutCamera()
    {
        _zoomCamera.Priority = 30;
        _isOut = false;
        _isZoom = true;
        Debug.Log("推された");
    }
}
