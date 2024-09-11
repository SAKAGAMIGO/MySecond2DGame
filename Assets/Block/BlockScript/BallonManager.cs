using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonManager : MonoBehaviour
{
    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 8;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;
        Debug.Log(DieVelocity);

        if (DieVelocity <= 8f)
        {
            _rb.isKinematic = false;

        }

        if (DieVelocity <= 2.5f)
        {
            // GetComponent<Renderer>().material.color = Color.red;
        }
        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Debug.Log(collision.relativeVelocity.sqrMagnitude);
            Destroy(this.gameObject);
        }
    }

    public void OnDestroy()
    {
        ScoreManager.AddScore(100);
    }
}
