using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expl_Des : MonoBehaviour
{
    private float m_Length;
    private float m_Cur;

    void Start()
    {
        Animator animOne = GetComponent<Animator>();
        AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
        m_Length = infAnim.length;
        m_Cur = 0;
    }

    void Update()
    {
        m_Cur += Time.deltaTime;
        if (m_Cur > m_Length)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
