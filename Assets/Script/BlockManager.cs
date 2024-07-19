using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    GameController gameController;

    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 20;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
            DieVelocity--;

            if (DieVelocity <= 2.5f)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
            {
                Debug.Log(collision.relativeVelocity.sqrMagnitude);
                Destroy(this.gameObject);
            }
    }

    private void OnDestroy()
    {
        gameController.AddScore(500);
    }
}
