using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddForce : ItemBaceClass
{
    [SerializeField] GameObject _player;

    Player _playerScript;

    public void Start()
    {
        _playerScript = _player.GetComponent<Player>();
    }

    public override void Activate()
    {
        _playerScript.AddFlyForce();
    }
}
