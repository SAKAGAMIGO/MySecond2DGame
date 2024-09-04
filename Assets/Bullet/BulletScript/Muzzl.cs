using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzl : MonoBehaviour
{
    [SerializeField] GameObject _shootEffect;

    [SerializeField] GameObject m_effect = default;

    GameController m_controller;

    /// <summary>パーティクルシステムの再生の真偽</summary>
    public bool _flag = true;

    public void ToShoot()
    {
        //ShootEffectを出現
        Instantiate(_shootEffect, transform.position, transform.rotation);
        Debug.Log("Object instantiated: " + _shootEffect.name);
    }

    public void Cherge()
    {
        if (m_effect)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }
    }

    private void Start()
    {
        m_controller = GameObject.FindAnyObjectByType<GameController>();
    }

    public void Update()
    {
        if (m_controller._isPlayerCount == true && _flag == true)
        {
            Cherge();
            _flag = false;
        }
    }
}
