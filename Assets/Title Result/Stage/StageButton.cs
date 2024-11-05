    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public Button stageButton; // ボタン
    public CharacterMover characterMover; // キャラクターのスクリプト参照

    void Start()
    {
        // ボタンがクリックされたときにキャラクターを移動させる
        //stageButton.onClick.AddListener(() => characterMover.MoveTo(newPosition));
    }
}
