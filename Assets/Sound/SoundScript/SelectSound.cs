using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.StageSelect);
    }
}
