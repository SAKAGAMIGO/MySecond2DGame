using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzl : MonoBehaviour
{
    [SerializeField] GameObject _shootEffect;

    public void ToShoot()
    {
        //ShootEffect���o��
        Instantiate(_shootEffect, transform.position, transform.rotation);
        Debug.Log("Object instantiated: " + _shootEffect.name);
    }
}
