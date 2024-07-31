using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;



public abstract class ItemBaceClass : MonoBehaviour
{
    GameController gameController;

    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 10;

    /// <summary>�A�C�e���̌��ʂ����������邩</summary>
    [Tooltip("Get ��I�ԂƁA��������Ɍ��ʂ���������BUse ��I�ԂƁA�A�C�e�����g�������ɔ�������")]
    [SerializeField] protected ActivateTiming _whenActivated = ActivateTiming.Get;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;

        GetItem(collision);
    }

    public virtual void GetItem(Collision2D collision)
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
                FindObjectOfType<GameController>().GetItem(ItemType.None, this);
                Debug.Log("GameController�ɃA�C�e����n��");
            }
        }
    }

    /// <summary>
    /// �A�C�e��������������ʂ���������
    /// </summary>
    public virtual void Activate()
    {
        Debug.LogError("�h���N���X�Ń��\�b�h���I�[�o�[���C�h���Ă��������B");
    }


    /// <summary>
    /// �A�C�e�������A�N�e�B�x�[�g���邩
    /// </summary>
    public enum ActivateTiming
    {
        /// <summary>��������ɂ����g��</summary>
        Get,
        /// <summary>�u�g���v�R�}���h�Ŏg��</summary>
        Use,
    }
}

public enum ItemType
{
    None,
    Fly,
    TNT
}
