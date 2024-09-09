using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Player‚ÌÅ‘åHP
    float _health = 0f;
    //public float HP => _health;

    //Player‚Ì‘Ì—Í
    HealthGuage _healthGuage;

    GameController _controller;


    void Start()
    {
        //HealthGuage‚ğæ“¾
        _healthGuage = GameObject.FindAnyObjectByType<HealthGuage>();
        //Setup‚ÉÅ‘åHP‚ğæ“¾
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
