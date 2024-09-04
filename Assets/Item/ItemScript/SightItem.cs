using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightItem : ItemBaceClass
{
    Player _playerScript;

    private void Start()
    {
        _playerScript = Player.FindObjectOfType<Player>();
    }

    public override void Activate()
    {
        _playerScript.AddDotLength();
    }
    public override void GetItem(Collision2D collision)
    {
        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            // �A�C�e�������^�C�~���O�ɂ���ď����𕪂���
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            else if (_whenActivated == ActivateTiming.Use)
            {
                // �����Ȃ����Ɉړ�����
                this.transform.position = Camera.main.transform.position;
                // �R���C�_�[�𖳌��ɂ���
                GetComponent<Collider2D>().enabled = false;
                //GameController�ɃA�C�e����n��
                FindObjectOfType<GameController>().GetItem(ItemType.Sight, this);
                Debug.Log("GameController�ɃA�C�e����n��");
            }
        }
    }
}
