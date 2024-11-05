using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private void Start()
    {
        LoadSelectedPosition();
    }

    private void LoadSelectedPosition()
    {
        if (PlayerPrefs.HasKey("SelectedPositionX") &&
            PlayerPrefs.HasKey("SelectedPositionY") &&
            PlayerPrefs.HasKey("SelectedPositionZ"))
        {
            float x = PlayerPrefs.GetFloat("SelectedPositionX");
            float y = PlayerPrefs.GetFloat("SelectedPositionY");
            float z = PlayerPrefs.GetFloat("SelectedPositionZ");
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            // 初期位置を設定（必要に応じて）
            transform.position = Vector3.zero; // またはデフォルトの位置を設定
        }
    }
}
