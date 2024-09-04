using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField, Tooltip("BGM")] AudioSource bgmAudioSource;
    [SerializeField, Tooltip("SE")] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    public float m_masterVolume = 1;
    public float m_bgmMasterVolume = 1;
    public float m_seMasterVolume = 1;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * m_bgmMasterVolume * m_masterVolume;
        bgmAudioSource.Play();
    }


    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * m_seMasterVolume * m_masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        StageSelect,
        Stage1,
        Stage2,
        Stagw3,
        Stage4,
        Stage5,
        Result,
        GameOver,
        GameClear// ���ꂪ���x���ɂȂ�
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Set,
        Shoot,
        Damage,
        Rock,
        RockBroken,
        Wood,
        WoodBroken,
        Ice,
        IceBroken,
        BallonBroken,
        Electric,
        Bomb,
        Player,
        Enemy,
        EnemyBroken,
        Boss,
        Cherge,
        Item,// ���ꂪ���x���ɂȂ�
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}
