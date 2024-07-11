using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grass : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    GameObject grass;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.isKinematic = true;
        grass = GameObject.Find("Grass");
        grass.GetComponent<Transform>();

    }


    void Update()
    {
        if (grass.transform.position.y <= 1.5f)
        {
            rigidbody2D.isKinematic = false;
        }
    }
}
