using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>死ぬ速度</summary>
    [SerializeField] float DieVelocity = 10;

    GameController gameController;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>衝突イベント</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if(collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Debug.Log(collision.relativeVelocity.sqrMagnitude);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameController.EnemyScore();
    }
}
