using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音の素材の種類
/// </summary>
public enum AudioClipType
{
    BreakTileSE,
    ResultSequenceSE,
}

/// <summary>
/// 音を操作する
/// </summary>
public class SoundController : MonoBehaviour
{
    // 瓦の画像を変えるクラス
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // シーン遷移を操作
    [SerializeField]
    SequenceController sequenceController = default;

    // 音を再生する場所
    [SerializeField]
    AudioSource audioSource = default;

    // 音の素材
    [SerializeField]
    List<AudioClip> audioCrip = default;

    ///// <summary>
    ///// 音を再生
    ///// </summary>
    ///// <param name="_audioClip">音の素材</param>
    public void PlaySound(AudioClipType _audioClipType)
    {
        audioSource.PlayOneShot(audioCrip[(int)_audioClipType]);
    }
}
