using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Player�̍ő�HP
    float _health = 0f;
    //public float HP => _health;

    //Player�̗̑�
    HealthGuage _healthGuage;

    GameController _controller;


    void Start()
    {
        //HealthGuage���擾
        _healthGuage = GameObject.FindAnyObjectByType<HealthGuage>();
        //Setup���ɍő�HP���擾
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
