using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音を操作する
/// </summary>
public class SoundController : MonoBehaviour
{
    /// <summary>
    /// 音の素材の種類
    /// </summary>
    public enum AudioClipTypeSE
    {
        BreakTileSE,
        ResultSequenceSE,
    }

    /// <summary>
    /// 音の素材の種類
    /// </summary>
    public enum AudioClipTypeBGM
    {
        TitleBGM,
        GamePlayBGM,
        ResultBGM,
    }

    // 効果音を再生する場所
    [SerializeField]
    AudioSource audioSourceSE = default;

    // BGMを再生する場所
    [SerializeField]
    AudioSource audioSourceBGM = default;

    // 効果音の素材
    [SerializeField]
    List<AudioClip> audioClipSE = default;

    // BGMの素材
    [SerializeField]
    List<AudioClip> audioClipBGM = default;

    /// <summary>
    /// 効果音を再生
    /// </summary>
    /// <param name="audioClipType">音の素材</param>
    public void PlaySE(AudioClipTypeSE audioClipType)
    {
        audioSourceSE.PlayOneShot(audioClipSE[(int)audioClipType]);
    }

    /// <summary>
    /// BGMを再生
    /// </summary>
    /// <param name="audioClipType">音の素材</param>
    public void PlayBGM(AudioClipTypeBGM audioClipType)
    {
        audioSourceBGM.clip = audioClipBGM[(int)audioClipType];
        audioSourceBGM.Play();
    }
}
