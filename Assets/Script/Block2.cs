using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block2 : BlockManager
{

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.useGravity = true;
        }
    }
}
