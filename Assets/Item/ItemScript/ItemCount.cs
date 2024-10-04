using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCount : MonoBehaviour
{
    public int _flyItemCount; // �A�C�e���̎c�萔
    public Text _flyItemCountText; // �c�萔��\������UI��Text�R���|�[�l���g
    public string _flyItemKey = "itemCount"; // PlayerPrefs�Ŏg���L�[

    public int _tntItemCount; // �A�C�e���̎c�萔
    public Text _tntItemCountText; // �c�萔��\������UI��Text�R���|�[�l���g
    public string _tntItemKey = "itemCount"; // PlayerPrefs�Ŏg���L�[

    void Start()
    {
        // PlayerPrefs����A�C�e���̎c�萔��ǂݍ��ށi����Ȃ�f�t�H���g�l�Ƃ���5��ݒ�j
        _flyItemCount = PlayerPrefs.GetInt(_flyItemKey, 3);
        UpdateItemCountUI();
        // PlayerPrefs����A�C�e���̎c�萔��ǂݍ��ށi����Ȃ�f�t�H���g�l�Ƃ���5��ݒ�j
        _tntItemCount = PlayerPrefs.GetInt(_tntItemKey, 3);
        _UpdateItemCountUI();
    }

    public void UpdateItemCountUI()
    {
        // �c�萔��Text�ɔ��f
        _flyItemCountText.text = "�c��: " + _flyItemCount.ToString();
    }

    public void _UpdateItemCountUI()
    {
        // �c�萔��Text�ɔ��f
        _tntItemCountText.text = "�c��: " + _flyItemCount.ToString();
    }

    public void ResetItemCount()
    {
        // �A�C�e���������Z�b�g����
        PlayerPrefs.DeleteKey(_flyItemKey); // �ۑ����ꂽ�l���폜
        _flyItemCount = 3; // �l��0�Ƀ��Z�b�g
        UpdateItemCountUI();
        PlayerPrefs.DeleteKey(_tntItemKey);
        _tntItemCount = 3;
        _UpdateItemCountUI();
    }

    private void LoadItemCount()
    {
        // �ۑ����ꂽ�A�C�e������ǂݍ��� (�f�t�H���g�l��0)
        _flyItemCount = PlayerPrefs.GetInt(_flyItemKey, 0);
        _tntItemCount = PlayerPrefs.GetInt(_tntItemKey, 0);
    }

    private void SaveItemCount()
    {
        // �A�C�e������ۑ�
        PlayerPrefs.SetInt(_flyItemKey, _flyItemCount);
        PlayerPrefs.Save(); // �ۑ����m��
        PlayerPrefs.SetInt(_tntItemKey, _tntItemCount);
    }




}
