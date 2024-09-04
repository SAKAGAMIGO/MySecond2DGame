using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>���ʑ��x</summary>
    [SerializeField] float DieVelocity = 10;

    GameController gameController;

    [SerializeField] GameObject m_effect = default;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if(collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Debug.Log(collision.relativeVelocity.sqrMagnitude);
            Destroy(this.gameObject);
            Hit();
        }
    }

    private void OnDestroy()
    {
        gameController.EnemyScore();
    }

    private void Hit()
    {
        if (m_effect)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}
