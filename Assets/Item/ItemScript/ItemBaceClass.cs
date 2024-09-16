using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using static SceneChenge;



public abstract class ItemBaceClass : MonoBehaviour
{
    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 10;

    /// <summary>�A�C�e���̌��ʂ����������邩</summary>
    [Tooltip("Get ��I�ԂƁA��������Ɍ��ʂ���������BUse ��I�ԂƁA�A�C�e�����g�������ɔ�������")]
    [SerializeField] protected ActivateTiming _whenActivated = ActivateTiming.Get;

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;
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
        TNT,
        Sight
}

