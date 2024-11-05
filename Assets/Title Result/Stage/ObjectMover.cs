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
            // �����ʒu��ݒ�i�K�v�ɉ����āj
            transform.position = Vector3.zero; // �܂��̓f�t�H���g�̈ʒu��ݒ�
        }
    }
}
