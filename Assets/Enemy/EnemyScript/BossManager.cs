using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : EnemyManager
{
    //Player�̍ő�HP
    float _health = 500f;
    public float HP => _health;

    //Player�̗̑�
    BossHealthGauge _bossHealthGauge;

    void Start()
    {
        //HealthGuage���擾
        _bossHealthGauge = GameObject.FindAnyObjectByType<BossHealthGauge>();
        //Setup���ɍő�HP���擾
        _bossHealthGauge.SetupBoss(_health);
    }

    public void AddDamageBoss(float damage)
    {
        _health -= damage;
        _bossHealthGauge.TakeDamageBoss(damage);
        //CinemaChine�̃p�����[�^��impulseSource�Ɋi�[
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        //�h�炷�������N��
        impulseSource.GenerateImpulse();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _health -= collision.relativeVelocity.sqrMagnitude;
    }
}