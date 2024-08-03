using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : EnemyManager
{
    //Player�̍ő�HP
    float _health = 100f;
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
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.GenerateImpulse();
    }
}
