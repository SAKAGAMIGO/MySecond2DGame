using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSound : MonoBehaviour
{
    public void OnDestroy()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.IceBroken);
    }
}
