using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルへのシーン遷移
/// </summary>
public class TitleTransition : MonoBehaviour
{
    // シーケンスのアニメーター
    [SerializeField]
    Animator sequenceAnimator = default;

    // 一回クリックできる状態か
    bool isStateClickableOnce = false;

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        isStateClickableOnce = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // マウスクリックを離した、かつ一回クリックできる状態ならタイトルへ遷移する
        if (Input.GetMouseButtonUp(0) && isStateClickableOnce)
        {
            sequenceAnimator.SetTrigger("isTitleScene");
            isStateClickableOnce = false;
        }
    }
}
