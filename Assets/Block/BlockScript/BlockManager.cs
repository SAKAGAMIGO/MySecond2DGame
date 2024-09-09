using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    ScoreManager _scoreManager;

    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 20;

    [SerializeField] GameObject m_effect = default;

    void Start()
    {
        _scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if (DieVelocity <= 8f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Hit();
        }
    }

    public void OnDestroy()
    {
        _scoreManager.AddScore(500);
        int _score = _scoreManager.GetCurrentScore();
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
