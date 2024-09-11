    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public Button stageButton; // ボタン
    public CharacterMover characterMover; // キャラクターのスクリプト参照
    public Transform stagePosition; // ステージの位置

    void Start()
    {
        // ボタンがクリックされたときにキャラクターを移動させる
        stageButton.onClick.AddListener(() => characterMover.MoveTo(stagePosition.position));
    }
}
