using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトからタイトルへのシーン遷移
/// </summary>
public class TitleFromResult : MonoBehaviour
{
    // シーケンスのアニメーター
    [SerializeField]
    Animator sequenceAnimator = default;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // マウスを離したらタイトルへシーン遷移
        if (Input.GetMouseButtonUp(0))
        {
            sequenceAnimator.SetTrigger("isTitleScene");
        }
    }
}
