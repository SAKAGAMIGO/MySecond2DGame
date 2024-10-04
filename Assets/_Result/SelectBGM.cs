using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.StageSelect);
    }
}
