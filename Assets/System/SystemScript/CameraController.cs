using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>Zoom�{�^��</summary>
    [SerializeField] GameObject _outButton;
    [SerializeField] GameObject _zoomButton;

    /// <summary>Zoom,Out�^�U</summary>
    bool _isZoom;
    bool _isOut;

    /// <summary>VacualCamera</summary>
    [SerializeField] CinemachineVirtualCamera _zoomCamera;
    [SerializeField] CinemachineVirtualCamera _camera1;
    [SerializeField] CinemachineVirtualCamera _camera2;

    GameController _controller;

    private Animator _cAnimator;
    private Animator _tAnimator;
    private Animator _pAnimator;

    public void Start()
    {
        _outButton.SetActive(true);
        _zoomButton.SetActive(false);
        _isZoom = false;
        _isOut = true;
        _controller = GameObject.FindObjectOfType<GameController>();
        //PlayerMotion�̃A�j���[�V�������i�[
        //_tAnimator = GameObject.Find("Target").GetComponent <Animator>();
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
            //_tAnimator.SetBool("Move", true);
        }      
    }

        //Zoom�{�^���A�N�e�B�u�Ǘ�
    public void Zoom()
    {
        if (_isZoom && _isOut == false)
        {
            _outButton.SetActive(false);
        }
        else
        {
            _outButton.SetActive(true);   
        }
    }

    //Out�{�^���A�N�e�B�u�Ǘ�
    public void Out()
    {
        if (_isOut && _isZoom == false)
        {
            _zoomButton.SetActive(false);
        }
        else
        {
            _zoomButton.SetActive(true);
        }
    }

    //VCameraIdol�̗D��x�ύX
    public void ZoomCamera()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        _zoomCamera.Priority = 30;
        _isZoom = false;
        _isOut = true;
        Debug.Log("�����ꂽ");
    }

    public void OutCamera()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Button);
        _zoomCamera.Priority = 0;
        _isZoom = true;
        _isOut = false;
        Debug.Log("�����ꂽ");
    }
}
