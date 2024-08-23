using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveYBlock: MonoBehaviour
{
    public int speed;
    private Vector3 startPos;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Žæ“¾");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(transform.position);
        float posY = startPos.y + Mathf.PingPong(Time.time * speed, 3f);
        rb.MovePosition(new Vector3(startPos.x, posY, startPos.z));
    }
}


