using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHealthGauge : MonoBehaviour
{
    //�ΐF�̃o�[
    [SerializeField] private Image _healthImage;
    //�ԐF�̃o�[
    [SerializeField] private Image _burnImage;
    //HP�����鎞��
    public float _duration = 0.5f;
    //�ő�HP
    private float _currentHP = 100f;
    //�o�[�������
    public float _damage = 10f;
    //�o�[���h��鋭��
    public float _strength = 20f;
    //�o�[���h��鋭��
    public int _vibrate = 100;

    private float _maxHp;

    public void SetupBoss(float hp)
    {
        _currentHP = hp;
        _maxHp = hp;
    }

    //HealthGauge���w�肵���p�����[�^�܂Ō��炷
    public void SetGuageBoss(float targetRate)
    {
        _healthImage.DOFillAmount(targetRate, _duration).OnComplete(() =>
        {
            _burnImage.DOFillAmount(targetRate, _duration * 0.5f).SetDelay(0.5f);
        });
        transform.DOShakePosition(_duration * 0.5f, _strength, _vibrate);
    }

    //HP����
    public void TakeDamageBoss(float rate)
    {
        SetGuageBoss((_currentHP - rate) / _maxHp);
        _currentHP -= rate;
        Debug.Log($"Rate: {rate}, Current: {_currentHP}, Max: {_maxHp}");
    }
}
