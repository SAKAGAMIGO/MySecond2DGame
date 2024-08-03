using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : EnemyManager
{
    //Player‚ÌÅ‘åHP
    float _health = 100f;
    public float HP => _health;

    //Player‚Ì‘Ì—Í
    BossHealthGauge _bossHealthGauge;

    void Start()
    {
        //HealthGuage‚ğæ“¾
        _bossHealthGauge = GameObject.FindAnyObjectByType<BossHealthGauge>();
        //Setup‚ÉÅ‘åHP‚ğæ“¾
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
