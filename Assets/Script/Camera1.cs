using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    [SerializeField]GameObject Player;

    [SerializeField]Vector3 lastPlayerPos;

    [SerializeField]float _moveSpeed;


    void Start()
    {
        lastPlayerPos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2((Player.transform.position.x - lastPlayerPos.x) * _moveSpeed, 0));

        lastPlayerPos = Player.transform.position;
    }
}
