using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音を操作する
/// </summary>
public class SoundController : MonoBehaviour
{
    // 瓦の画像を変えるクラス
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // リザルトオブジェクトがアクティブになったか確認
    [SerializeField]
    OnResultObjectActiveCheck onResultObjectActiveCheck = default;

    // オーディオソース
    [SerializeField]
    AudioSource audioSource = default;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].InitPlayBreakTileSound(PlaySound);
        }

        onResultObjectActiveCheck.InitPlayResultSound(PlaySound);
    }

    /// <summary>
    /// 音を流す
    /// </summary>
    /// <param name="_audioClip">音の素材</param>
    void PlaySound(AudioClip _audioClip)
    {
        audioSource.PlayOneShot(_audioClip);
    }
}
