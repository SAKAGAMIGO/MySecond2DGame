using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{ 
    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 20;

    [SerializeField] GameObject m_effect = default;

    void Start()
    {
        
    }

    /// <summary>衝突イベント</summary><param name="collision"></param>
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
        SceneChenge.AddScore(500);
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
