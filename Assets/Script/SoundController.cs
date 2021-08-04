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
    }

    // 音を再生する場所
    [SerializeField]
    AudioSource audioSource = default;

    // 音の素材
    [SerializeField]
    List<AudioClip> audioClip = default;

    /// <summary>
    /// 音を再生
    /// </summary>
    /// <param name="audioClipType">音の素材</param>
    public void PlaySound(AudioClipType audioClipType)
    {
        audioSource.PlayOneShot(audioClip[(int)audioClipType]);
    }
}
