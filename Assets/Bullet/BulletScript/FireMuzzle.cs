using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMuzzle : MonoBehaviour
{
    [SerializeField] GameObject m_effect = default;

    bool _flag = true;

    Player m_player;

    public void ToFire()
    {
        Instantiate(m_effect, transform.position, Quaternion.identity);
    }

    void Start()
    {
        m_player = Object.FindObjectOfType<Player>();
    }


}
