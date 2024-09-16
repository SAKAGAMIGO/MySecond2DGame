using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthGuage : MonoBehaviour
{
    //�ԐF�̃o�[
    [SerializeField] private Image _burn;

    //HP�Q�[�W�̃p�����[�^
    [Tooltip("HP�����鎞��")] float _duration = 0.5f;
    [Tooltip("�ő�HP")] float _maxHp;
    [Tooltip("���݂�HP")] float _currentHP = 100f;
    [Tooltip("����")] float _strength = 20f;
    [Tooltip("�h���傫��")] int _vibrate = 100;

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

    //HP������v���O����
    public void TakeDamage(float rate)
    {
        SetGuage((_currentHP + rate) / _maxHp);
        _currentHP += rate;
    }
}
