using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Player‚ÌÅ‘åHP
    float _health = 100f;
    public float HP => _health;

    //Player‚Ì‘Ì—Í
    HealthGuage _healthGuage;

    void Start()
    {
        //HealthGuage‚ğæ“¾
        _healthGuage = GameObject.FindAnyObjectByType<HealthGuage>();
        //Setup‚ÉÅ‘åHP‚ğæ“¾
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
