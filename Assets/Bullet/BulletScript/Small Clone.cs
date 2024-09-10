using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SmallClone : MonoBehaviour
{
    BlockManager _block;
    EnemyManager _enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            float _dieVerocity = collision.relativeVelocity.sqrMagnitude;

            BlockManager _block = collision.gameObject.GetComponent<BlockManager>();
            EnemyManager _enemy = collision.gameObject.GetComponent<EnemyManager>();

            _block.DieVelocity -= 10;
            _enemy.DieVelocity -= 10;
        }
    }
}
