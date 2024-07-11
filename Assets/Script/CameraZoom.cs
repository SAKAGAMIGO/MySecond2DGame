using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    //Main Camera��Transform
    Transform cameraTransform;

    //Main Camera��Camera
    Camera camera;

    //�Y�[���C���A�Y�[���A�E�g����
    bool zoomCamera;
    bool outCamera;

    public GameObject zoomText;
    public GameObject outText;

    void Start()
    {
        zoomCamera = true; //�Y�[���C���\�ɂ���
        outCamera = false; //�Y�[���A�E�g�s�\�ɂ���
        outText.SetActive(false);
        cameraTransform = this.gameObject.GetComponent<Transform>(); //Main Camera��Transform���擾����
        camera = this.gameObject.GetComponent<Camera>(); //Main Camera��Camera���擾����
    }

    void Update()
    {
        //I�L�[��������Ă��邩�Y�[���C�����\�ȂƂ�
        if (Input.GetKey(KeyCode.I) && zoomCamera == true)
        {
            Zoom();
            zoomCamera = false;
            zoomText.SetActive(false);
            outText.SetActive(true);
        }
        //O�L�[��������Ă��邩�Y�[���A�E�g���\�ȂƂ�
        else if (Input.GetKey(KeyCode.O) && outCamera == true)
        {
            Out();
            outText.SetActive(false);
            zoomText.SetActive(true);
        }
    }

    public void Zoom()
    {
        camera.orthographicSize = camera.orthographicSize - 0.5f; //�Y�[���C��
        zoomCamera = false; //�Y�[���C���s�\
        outCamera = true; //�Y�[���A�E�g�\
        zoomText.SetActive(false);
        outText.SetActive(true);
        Debug.Log("Zoom");
    }

    public void Out()
    {
        camera.orthographicSize = camera.orthographicSize + 0.5f; //�Y�[���A�E�g
        outCamera = false; //�Y�[���A�E�g�s�\
        zoomCamera = true; //�Y�[���C���\
        outText.SetActive(false);
        zoomText.SetActive(true);
        Debug.Log("Out");
    }
}