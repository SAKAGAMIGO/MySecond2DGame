using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightItem : ItemBaceClass
{
    Player _player;

    public void AddPlayer(Player player)
    {
        _player = player;
    }

    public override void Activate()
    {
        _player.AddDotLength();
    }
}
