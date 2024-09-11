using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ItemType _itemType;

    GameController _controller;

    private void Start()
    {
        _controller = FindAnyObjectByType<GameController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Item);
        _controller.UseItem(_itemType);
        Debug.Log($"Use{_itemType}");
    }
}
