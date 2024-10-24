using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Playerの最大HP
    float _health = 0f;
    //public float HP => _health;

    //Playerの体力
    HealthGuage _healthGuage;

    GameController _controller;

    void Start()
    {
        //HealthGuageを取得
        _healthGuage = GameObject.FindAnyObjectByType<HealthGuage>();
        //Setup時に最大HPを取得
        _healthGuage.Setup(0);
        _controller = GameObject.FindAnyObjectByType<GameController>();
    }

    public void AddDamage(float damage)
    {
        _health += damage;
        _healthGuage.TakeDamage(damage);
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        //impulseSource.GenerateImpulse();
    }

    private void Update()
    {
        if (_health == 100)
        {
            _controller._isGameOver = true;
        }
    }
}
