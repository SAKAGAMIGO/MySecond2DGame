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
    [SerializeField] CinemachineVirtualCamera _defaltCamera;

    public void Start()
    {
        _zoomButton.SetActive(true);
        _outButton.SetActive(false);
        _isZoom = true;
        _isOut = false;
    }

    public void Update()
    {
        Zoom();
        Out();
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
        _defaltCamera.Priority = 0;
        _isZoom = false;
        _isOut = true;
        Debug.Log("推された");
    }
    public void OutCamera()
    {
        _defaltCamera.Priority = 20;
        _isOut = false;
        _isZoom = true;
        Debug.Log("推された");
    }
}
