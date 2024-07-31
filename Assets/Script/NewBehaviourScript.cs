using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject _shootEffect;

    Player _playerCom;

    void Start()
    {
        _playerCom = GameObject.FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //ShootEffectÇèoåª
        Instantiate(_shootEffect, transform.position, transform.rotation);
    }
}
