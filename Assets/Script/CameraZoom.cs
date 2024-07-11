using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    //Main CameraのTransform
    Transform cameraTransform;

    //Main CameraのCamera
    Camera camera;

    //ズームイン、ズームアウト判定
    bool zoomCamera;
    bool outCamera;

    public GameObject zoomText;
    public GameObject outText;

    void Start()
    {
        zoomCamera = true; //ズームイン可能にする
        outCamera = false; //ズームアウト不可能にする
        outText.SetActive(false);
        cameraTransform = this.gameObject.GetComponent<Transform>(); //Main CameraのTransformを取得する
        camera = this.gameObject.GetComponent<Camera>(); //Main CameraのCameraを取得する
    }

    void Update()
    {
        //Iキーが押されているかつズームインが可能なとき
        if (Input.GetKey(KeyCode.I) && zoomCamera == true)
        {
            Zoom();
            zoomCamera = false;
            zoomText.SetActive(false);
            outText.SetActive(true);
        }
        //Oキーが押されているかつズームアウトが可能なとき
        else if (Input.GetKey(KeyCode.O) && outCamera == true)
        {
            Out();
            outText.SetActive(false);
            zoomText.SetActive(true);
        }
    }

    public void Zoom()
    {
        camera.orthographicSize = camera.orthographicSize - 0.5f; //ズームイン
        zoomCamera = false; //ズームイン不可能
        outCamera = true; //ズームアウト可能
        zoomText.SetActive(false);
        outText.SetActive(true);
        Debug.Log("Zoom");
    }

    public void Out()
    {
        camera.orthographicSize = camera.orthographicSize + 0.5f; //ズームアウト
        outCamera = false; //ズームアウト不可能
        zoomCamera = true; //ズームイン可能
        outText.SetActive(false);
        zoomText.SetActive(true);
        Debug.Log("Out");
    }
}