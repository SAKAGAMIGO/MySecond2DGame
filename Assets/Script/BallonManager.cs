using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonManager : MonoBehaviour
{
    ScoreManager _scoreManager;

    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 8;

    Rigidbody2D _rb;

    void Start()
    {
        _scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
    }

    /// <summary>衝突イベント</summary><param name="collision"></param>
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
        _scoreManager.AddScore(100);

        int _score = _scoreManager.GetCurrentScore();
    }
}
