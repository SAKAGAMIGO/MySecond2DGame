using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSound : MonoBehaviour
{
    private void OnDestroy()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.WoodBroken);
    }
}
