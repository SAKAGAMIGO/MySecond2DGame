using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCount : MonoBehaviour
{
    public int _flyItemCount; // アイテムの残り数
    public Text _flyItemCountText; // 残り数を表示するUIのTextコンポーネント
    public string _flyItemKey = "itemCount"; // PlayerPrefsで使うキー

    public int _tntItemCount; // アイテムの残り数
    public Text _tntItemCountText; // 残り数を表示するUIのTextコンポーネント
    public string _tntItemKey = "itemCount"; // PlayerPrefsで使うキー

    void Start()
    {
        // PlayerPrefsからアイテムの残り数を読み込む（初回ならデフォルト値として5を設定）
        _flyItemCount = PlayerPrefs.GetInt(_flyItemKey, 3);
        UpdateItemCountUI();
        // PlayerPrefsからアイテムの残り数を読み込む（初回ならデフォルト値として5を設定）
        _tntItemCount = PlayerPrefs.GetInt(_tntItemKey, 3);
        _UpdateItemCountUI();
    }

    public void UpdateItemCountUI()
    {
        // 残り数をTextに反映
        _flyItemCountText.text = "残り: " + _flyItemCount.ToString();
    }

    public void _UpdateItemCountUI()
    {
        // 残り数をTextに反映
        _tntItemCountText.text = "残り: " + _flyItemCount.ToString();
    }

    public void ResetItemCount()
    {
        // アイテム数をリセットする
        PlayerPrefs.DeleteKey(_flyItemKey); // 保存された値を削除
        _flyItemCount = 3; // 値を0にリセット
        UpdateItemCountUI();
        PlayerPrefs.DeleteKey(_tntItemKey);
        _tntItemCount = 3;
        _UpdateItemCountUI();
    }

    private void LoadItemCount()
    {
        // 保存されたアイテム数を読み込む (デフォルト値は0)
        _flyItemCount = PlayerPrefs.GetInt(_flyItemKey, 0);
        _tntItemCount = PlayerPrefs.GetInt(_tntItemKey, 0);
    }

    private void SaveItemCount()
    {
        // アイテム数を保存
        PlayerPrefs.SetInt(_flyItemKey, _flyItemCount);
        PlayerPrefs.Save(); // 保存を確定
        PlayerPrefs.SetInt(_tntItemKey, _tntItemCount);
    }




}
