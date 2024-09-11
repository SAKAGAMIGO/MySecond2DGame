using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BGMSoundData;

public class Stage1BGM : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage);
    }

    void Update()
    {
        
    }
}
