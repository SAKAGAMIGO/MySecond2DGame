using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator _animation;
    public GameObject _door;
    BoxCollider2D _boxCollider;

    void Start()
    {
        _door = GameObject.Find("Door");
        _boxCollider = _door.GetComponent<BoxCollider2D>();
        _animation = _door.GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        _animation.SetBool("Open", true);
        _boxCollider.enabled = false;
        Debug.Log("Door‚Ì“–‚½‚è”»’è‚ð–³‚­‚·");
        Destroy(this.gameObject);
    }
}
