using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthGuage : MonoBehaviour
{
    //赤色のバー
    [SerializeField] private Image _burn;

    //HPゲージのパラメータ
    [Tooltip("HPが減る時間")] float _duration = 0.5f;
    [Tooltip("最大HP")] float _maxHp;
    [Tooltip("現在のHP")] float _currentHP = 100f;
    [Tooltip("強さ")] float _strength = 20f;
    [Tooltip("揺れる大きさ")] int _vibrate = 100;

    GameController _controller;

    private void Start()
    {
        
    }

    public void Setup(float hp)
    {
        _currentHP = hp;
        _maxHp = 100;
    }

    public void SetGuage(float targetRate)
    {
        _burn.DOFillAmount(targetRate, _duration * 0.5f).SetDelay(0.5f);
        transform.DOShakePosition(_duration * 0.5f, _strength, _vibrate);
    }

    //HPが減るプログラム
    public void TakeDamage(float rate)
    {
        SetGuage((_currentHP + rate) / _maxHp);
        _currentHP += rate;
    }
}
