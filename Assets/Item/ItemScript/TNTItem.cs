using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TNTItem : ItemBaceClass
{
    GameController _gameController;

    public void AddController(GameController controller)
    {
        _gameController = controller;
    }

    public override void Activate()
    {
        _gameController.AddTNT();
    }
}
