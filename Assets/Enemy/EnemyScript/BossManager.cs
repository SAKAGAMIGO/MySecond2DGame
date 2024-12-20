using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : EnemyManager
{
    //Playerの最大HP
    float _health = 500f;
    public float HP => _health;

    //Playerの体力
    BossHealthGauge _bossHealthGauge;

    void Start()
    {
        //HealthGuageを取得
        _bossHealthGauge = GameObject.FindAnyObjectByType<BossHealthGauge>();
        //Setup時に最大HPを取得
        _bossHealthGauge.SetupBoss(_health);
    }

    public void AddDamageBoss(float damage)
    {
        _health -= damage;
        _bossHealthGauge.TakeDamageBoss(damage);
        //CinemaChineのパラメータをimpulseSourceに格納
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        //揺らす処理を起動
        impulseSource.GenerateImpulse();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _health -= collision.relativeVelocity.sqrMagnitude;
    }
}
