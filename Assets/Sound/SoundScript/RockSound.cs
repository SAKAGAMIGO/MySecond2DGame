using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSound : MonoBehaviour
{
    private void OnDestroy()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.RockBroken);
    }
}
