using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    private Vector3 _diff; //�J�����ƃv���C���[�̋���
    private GameObject _target; //�Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    public float followSpeed; //�Ǐ]����X�s�[�h

    //�Y�[���C���A�Y�[���A�E�g����
    bool _isZoom;
    bool _isOut;

    [SerializeField] GameObject _zoomText;
    [SerializeField] GameObject _outText;

    void Start()
    {
        _outText.SetActive(false);
        _isZoom = true; //�Y�[���C���\�ɂ���
        _isOut = false; //�Y�[���A�E�g�s�\�ɂ���
    }

    public void Follow()
    {
        
        if (_isZoom = true)
        {
            Debug.Log("�t�H���[");
            _target = GameObject.Find("CameraTarget");//���O��Player�̃I�u�W�F�N�g���擾���ă^�[�Q�b�g�Ɏw��
            _diff = _target.transform.position - this.transform.position; //�J�����ƃv���C���[�̏����̋������w��
            transform.position = Vector3.Lerp(this.transform.position, _target.transform.position - _diff, Time.deltaTime * followSpeed); //���`��Ԋ֐��ɂ��J�����̈ړ�
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
        //I�L�[��������Ă��邩�Y�[���C�����\�ȂƂ�
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
