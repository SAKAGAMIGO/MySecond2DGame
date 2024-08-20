using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    void Update()
    {
        float T = 2.0f;
        float f = 1.0f / T;
        float sin = Mathf.Sin(2 * Mathf.PI * f * (Time.time * 1/2));
        this.transform.position = new Vector3(0, sin, 0);
    }
}
