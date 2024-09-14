using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SmallClone : MonoBehaviour
{
    BlockManager _block;
    EnemyManager _enemy;

    private void Start()
    {
        _block = Object.FindObjectOfType<BlockManager>();
        _enemy = Object.FindObjectOfType<EnemyManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            _block.DieVelocity -= 10;
            _enemy.DieVelocity -= 10;
        }
    }
}
