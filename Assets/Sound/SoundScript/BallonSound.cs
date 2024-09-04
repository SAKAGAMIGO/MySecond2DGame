using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSound : MonoBehaviour
{
    private void OnDestroy()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.BallonBroken);
    }
}
