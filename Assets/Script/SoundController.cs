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

        // 全ての瓦に割った音を流す関数を登録
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].InitPlayBreakTileSound(OnePlaySound);
        }

        // リザルトオブジェクトにリザルトに遷移した時の音を流す関数を登録
        onResultObjectActiveCheck.InitPlayResultSound(OnePlaySound);
    }

    /// <summary>
    /// 音を一回流す
    /// </summary>
    /// <param name="_audioClip">音の素材</param>
    void OnePlaySound(AudioClip _audioClip)
    {
        audioSource.PlayOneShot(_audioClip);
    }
}
