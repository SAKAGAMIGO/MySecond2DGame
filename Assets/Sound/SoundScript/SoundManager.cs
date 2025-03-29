using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField, Tooltip("BGM")] private AudioSource bgmAudioSource;
    [SerializeField, Tooltip("SE")] private AudioSource seAudioSourcePrefab;

    [SerializeField] private List<BGMSoundData> bgmSoundDatas;
    [SerializeField] private List<SESoundData> seSoundDatas;

    [Range(0, 1)] public float m_masterVolume = 1;
    [Range(0, 1)] public float m_bgmMasterVolume = 1;
    [Range(0, 1)] public float m_seMasterVolume = 1;

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
        BGMSoundData data = bgmSoundDatas.Find(d => d.bgm == bgm);
        if (data == null)
        {
            Debug.LogWarning($"BGM '{bgm}' not found!");
            return;
        }

        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * m_bgmMasterVolume * m_masterVolume;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }
    }

    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(d => d.se == se);
        if (data == null)
        {
            Debug.LogWarning($"SE '{se}' not found!");
            return;
        }

        AudioSource seAudioSource = Instantiate(seAudioSourcePrefab, transform);
        seAudioSource.clip = data.audioClip;
        seAudioSource.volume = data.volume * m_seMasterVolume * m_masterVolume;
        seAudioSource.Play();

        Destroy(seAudioSource.gameObject, data.audioClip.length);
    }

    public void UpdateVolume()
    {
        bgmAudioSource.volume = m_bgmMasterVolume * m_masterVolume;
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        StageSelect,
        Stage,
        Result,
        GameOver, // ‚±‚ê‚ªƒ‰ƒxƒ‹‚É‚È‚é
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)] public float volume = 1;
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
        TNT,
        Bomb,
        Player,
        Enemy,
        EnemyBroken,
        Boss,
        Charge,
        Item,
        Button, // ‚±‚ê‚ªƒ‰ƒxƒ‹‚É‚È‚é
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)] public float volume = 1;
}
