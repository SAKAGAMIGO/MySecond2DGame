using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SmallClone : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    public void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }
}
