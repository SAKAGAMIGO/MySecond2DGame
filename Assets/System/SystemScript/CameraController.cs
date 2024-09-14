using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>Zoomボタン</summary>
    [SerializeField] GameObject _outButton;
    [SerializeField] GameObject _zoomButton;

    [SerializeField, Tooltip("Zoom,Out真偽")]
    bool _isZoom;

    /// <summary>VacualCamera</summary>
    [SerializeField] CinemachineVirtualCamera _zoomCamera;
    [SerializeField] CinemachineVirtualCamera _camera1;
    [SerializeField] CinemachineVirtualCamera _camera2;

    GameController _controller;

    private Animator _pAnimator;

    public void Start()
    {
        _controller = GameObject.FindObjectOfType<GameController>();
        //PlayerMotionのアニメーションを格納
        _pAnimator = GameObject.Find("Man_Gun").GetComponent<Animator>();
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
        }
    }

    //Zoomボタンアクティブ管理
    public void Zoom()
    {
        if (_isZoom)
        {
            _outButton.SetActive(false);
            _zoomButton.SetActive(true);
        }
    }

    //Outボタンアクティブ管理
    public void Out()
    {
        if (_isZoom == false)
        {
            _outButton.SetActive(true);
            _zoomButton.SetActive(false);
        }
    }

    //VCameraIdolの優先度変更
    public void ZoomCamera()
    {
        _isZoom = false;
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        _zoomCamera.Priority = 30;
        Debug.Log("ズーム");
    }

    public void OutCamera()
    {
        _isZoom = true;
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        _zoomCamera.Priority = 0;
        Debug.Log("アウト");
    }
}
