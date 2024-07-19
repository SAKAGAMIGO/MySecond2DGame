using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TNTItem : ItemBaceClass
{
    [SerializeField] GameObject _gameController;

    GameController _controller;

    private void Start()
    {
        _controller = _gameController.GetComponent<GameController>();
    }

    public override void Activate()
    {
        _controller.AddTNT();
    }
}
