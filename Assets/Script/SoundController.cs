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
    public enum AudioClipType
    {
        BreakTileSE,
        ResultSequenceSE,
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

    // 音の素材
    [SerializeField]
    List<AudioClip> audioClip = default;

    /// <summary>
    /// 効果音を再生
    /// </summary>
    /// <param name="audioClipType">音の素材</param>
    public void PlaySE(AudioClipType audioClipType)
    {
        audioSourceSE.PlayOneShot(audioClip[(int)audioClipType]);
    }

    /// <summary>
    /// BGMを再生
    /// </summary>
    /// <param name="audioClipType">音の素材</param>
    public void PlayBGM(AudioClipType audioClipType)
    {
        audioSourceBGM.clip = audioClip[(int)audioClipType];
        audioSourceBGM.Play();
    }
}
