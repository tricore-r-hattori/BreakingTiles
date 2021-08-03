using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// リザルトオブジェクトがアクティブになったか確認
/// </summary>
public class OnResultObjectActiveCheck : MonoBehaviour
{
    // リザルトオブジェクトがアクティブになった時に効果音を流すためのAction
    event Action<AudioClip> onPlayResultSound;

    // リザルト遷移時の音
    [SerializeField]
    AudioClip resultAudioClip = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="_onPlayResultSound">// リザルトオブジェクトがアクティブになった時に効果音を流すためのAction</param>
    public void Init(Action<AudioClip> _onPlayResultSound)
    {
        this.onPlayResultSound = _onPlayResultSound;
    }

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // リザルトオブジェクトがアクティブになった時に効果音を流すためのAction
        onPlayResultSound(resultAudioClip);
    }
}
