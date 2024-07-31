using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzl : MonoBehaviour
{
    [SerializeField] GameObject _shootEffect;

    public void ToShoot()
    {
        //ShootEffectÇèoåª
        Instantiate(_shootEffect, transform.position, transform.rotation);
        Debug.Log("Object instantiated: " + _shootEffect.name);
    }
}
