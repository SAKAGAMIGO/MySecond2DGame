using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)]
    private float wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 10f)]
    private float moveSpeed = 0.3f;

    private Vector3 preMousePos;

    CinemachineVirtualCamera _camera;

    Transform _myTransform;

    public void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Update()
    {
        MouseUpdate();

        Transform _myTransForm = this.transform;

        Vector2 worldPos = _myTransForm.position;
        float x = worldPos.x;    // ワールド座標を基準にした、x座標が入っている変数
        float y = worldPos.y;
        Debug.Log(worldPos);

        if (worldPos.x < 0 && worldPos.x > 0)
        {
            worldPos.x = 0;
        }
        if (worldPos.y < 0 && worldPos.y > 0)
        {
            worldPos.y = 0;
        }

        return;
    }

    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0.0f)
        {
            _camera.m_Lens.OrthographicSize = MouseWheel(scrollWheel, _camera.m_Lens.OrthographicSize);
        }

        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
        {
            preMousePos = Input.mousePosition;
        }

        MouseDrag(Input.mousePosition);
    }

    /// <summary>マウスホイールイベント</summary><param name="delta"></param>
    private float MouseWheel(float delta, float size)
    {
        size -= delta * wheelSpeed;
        return Mathf.Clamp(size, 2, 5);

    }

    /// <summary>マウスドラックイベント</summary>
    /// <param name="mousePos"></param>
    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            transform.Translate(-diff * Time.deltaTime * moveSpeed);
        }

        preMousePos = mousePos;
    }
}
