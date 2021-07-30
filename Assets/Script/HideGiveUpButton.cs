using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ギブアップボタンを非表示にするためのクラス
/// </summary>
public class HideGiveUpButton : MonoBehaviour
{
    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // ギブアップボタン
    [SerializeField]
    GameObject giveUpButton = default;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールできる状態だったらギブアップボタンを非表示にする
        if (scrollControllObjectHitCheck.State == ScrollState.Scrollable)
        {
            giveUpButton.SetActive(false);
        }
    }
}
