using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Player�̍ő�HP
    float _health = 100f;
    public float HP => _health;

    //Player�̗̑�
    HealthGuage _healthGuage;

    void Start()
    {
        //HealthGuage���擾
        _healthGuage = GameObject.FindAnyObjectByType<HealthGuage>();
        //Setup���ɍő�HP���擾
        _healthGuage.Setup(_health);
    }

    public void AddDamage(float damage)
    {
        _health -= damage;
        _healthGuage.TakeDamage(damage);
        var impulseSource = GetComponent<CinemachineImpulseSource>();
        //impulseSource.GenerateImpulse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
