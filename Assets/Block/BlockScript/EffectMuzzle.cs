using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMuzzle : MonoBehaviour
{

    [SerializeField] GameObject m_effect = default;
    bool _isEffect = true;

    private void Update()
    {
        if (_isEffect)
        {
            Hit();
            _isEffect = false;
        }
        
    }
    
    

    private void Hit()
    {
        if (m_effect)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }
    }
}
