using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : EnemyManager
{
    //Player‚ÌÅ‘åHP
    float _health = 500f;
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
        //CinemaChine‚Ìƒpƒ‰ƒ[ƒ^‚ğimpulseSource‚ÉŠi”[
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        //—h‚ç‚·ˆ—‚ğ‹N“®
        impulseSource.GenerateImpulse();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _health -= collision.relativeVelocity.sqrMagnitude;
    }
}
