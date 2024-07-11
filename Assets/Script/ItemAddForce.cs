using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddForce : ItemBaceClass
{
    [SerializeField] GameObject _player;

    Player FlyAddForce;

    public void Start()
    {
        FlyAddForce = _player.GetComponent<Player>();
    }

    public override void Activate()
    {
        FlyAddForce.AddFlyForce();
    }
}
