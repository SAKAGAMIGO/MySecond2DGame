using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRelease : MonoBehaviour
{
    [SerializeField] Button _Stage2;
    [SerializeField] Button _Stage3;
    [SerializeField] Button _Stage4;
    [SerializeField] Button _Stage5;
    [SerializeField] Button _Stage6;

    public bool m_isSt2;
    public bool m_isSt3;
    public bool m_isSt4;
    public bool m_isSt5;
    public bool m_isSt6;


    private void Start()
    {
        _Stage2.gameObject.SetActive(false);
        _Stage3.gameObject.SetActive(false);
        _Stage4.gameObject.SetActive(false);
        _Stage5.gameObject.SetActive(false);
        _Stage6.gameObject.SetActive(false);
        m_isSt2 = false;
        m_isSt3 = false;
        m_isSt4 = false;
        m_isSt5 = false;
        m_isSt6 = false;
    }

    private void Update()
    {
        if (m_isSt2)
        {
            _Stage2.gameObject.SetActive(true);
        }
        else if (m_isSt3)
        {
            _Stage3.gameObject.SetActive(true);
        }
        else if (m_isSt4)
        {
            _Stage4.gameObject.SetActive(true);
        }
        else if (m_isSt5)
        {
            _Stage5.gameObject.SetActive(true);
        }
        else if (m_isSt6)
        {
            _Stage6.gameObject.SetActive(true);
        }
    }
}
