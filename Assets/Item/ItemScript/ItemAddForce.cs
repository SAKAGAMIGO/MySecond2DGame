using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddForce : ItemBaceClass
{
    Player _playerScript;

    public void Start()
    {
        _playerScript = Player.FindObjectOfType<Player>();
    }

    public override void Activate()
    {
        _playerScript.AddFlyForce();
    }
    public override void GetItem(Collision2D collision)
    {
        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            // アイテム発動タイミングによって処理を分ける
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            else if (_whenActivated == ActivateTiming.Use)
            {
                // 見えない所に移動する
                this.transform.position = Camera.main.transform.position;
                // コライダーを無効にする
                GetComponent<Collider2D>().enabled = false;
                //GameControllerにアイテムを渡す
                FindObjectOfType<GameController>().GetItem(ItemType.Fly, this);
                Debug.Log("GameControllerにアイテムを渡す");
            }
        }
    }
}
