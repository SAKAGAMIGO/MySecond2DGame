using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddForce : ItemBaceClass
{
    Player _player;

    public void Start()
    {
        
    }

    public void AddPlayer(Player player)
    {
        _player = player;
    }

    public override void Activate()
    {
        _player.AddFlyForce();
    }
}
