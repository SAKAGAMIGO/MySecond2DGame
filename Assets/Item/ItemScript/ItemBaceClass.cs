using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using static SceneChenge;



public abstract class ItemBaceClass : MonoBehaviour
{
    /// <summary>死ぬ速度</summary>
    public float DieVelocity = 10;

    /// <summary>アイテムの効果をいつ発揮するか</summary>
    [Tooltip("Get を選ぶと、取った時に効果が発動する。Use を選ぶと、アイテムを使った時に発動する")]
    [SerializeField] protected ActivateTiming _whenActivated = ActivateTiming.Get;

    /// <summary>衝突イベント</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity--;
    }

    /// <summary>
    /// アイテムが発動する効果を実装する
    /// </summary>
    public virtual void Activate()
    {
        Debug.LogError("派生クラスでメソッドをオーバーライドしてください。");
    }


    /// <summary>
    /// アイテムをいつアクティベートするか
    /// </summary>
    public enum ActivateTiming
    {
        /// <summary>取った時にすぐ使う</summary>
        Get,
        /// <summary>「使う」コマンドで使う</summary>
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

