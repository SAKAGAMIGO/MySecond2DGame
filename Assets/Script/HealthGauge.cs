using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthGuage : MonoBehaviour
{
    //緑色のバー
    [SerializeField] private Image _health;
    //赤色のバー
    [SerializeField] private Image _burn;
    //HPが減る時間
    public float _duration = 0.5f;
    //最大HP
    private float _currentHP = 100f;
    //バーが減る量
    public float _damage = 10f;
    //バーが揺れる強さ
    public float _strength = 20f;
    //バーが揺れる強さ
    public int _vibrate = 100;

    private float _maxHp;

    public void Setup(float hp)
    {
        _currentHP = hp;
        _maxHp = hp;
    }

    public void SetGuage(float targetRate)
    {
        _health.DOFillAmount(targetRate, _duration).OnComplete(() =>
        {
            _burn.DOFillAmount(targetRate, _duration * 0.5f).SetDelay(0.5f);
        });
        transform.DOShakePosition(_duration * 0.5f, _strength, _vibrate);
    }

    //HPが減るプログラム
    public void TakeDamage(float rate)
    {
        SetGuage((_currentHP - rate) / _maxHp);
        _currentHP -= rate;
        Debug.Log($"Rate: {rate}, Current: {_currentHP}, Max: {_maxHp}");
    }
}
