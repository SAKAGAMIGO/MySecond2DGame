using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    Animator _animation;
    public GameObject _doorRotation;
    BoxCollider2D _boxCollider;

    void Start()
    {
        _doorRotation = GameObject.FindWithTag("DoorRotation"); 
        _boxCollider = _doorRotation.GetComponent<BoxCollider2D>();
        _animation = _doorRotation.GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        _animation.SetBool("Rotation", true);
        _boxCollider.enabled = false;
        Debug.Log("Door‚Ì“–‚½‚è”»’è‚ð–³‚­‚·");
        Destroy(this.gameObject);
    }
}
