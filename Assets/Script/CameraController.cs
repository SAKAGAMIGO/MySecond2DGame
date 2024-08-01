using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>Zoom�{�^��</summary>
    [SerializeField] GameObject _zoomButton;
    [SerializeField] GameObject _outButton;

    /// <summary>Zoom,Out�^�U</summary>
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

        //Zoom�{�^���A�N�e�B�u�Ǘ�
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

    //Out�{�^���A�N�e�B�u�Ǘ�
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

    //VCameraIdol�̗D��x�ύX
    public void ZoomCamera()
    {
        _defaltCamera.Priority = 0;
        _isZoom = false;
        _isOut = true;
        Debug.Log("�����ꂽ");
    }
    public void OutCamera()
    {
        _defaltCamera.Priority = 20;
        _isOut = false;
        _isZoom = true;
        Debug.Log("�����ꂽ");
    }
}
