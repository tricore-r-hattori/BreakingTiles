using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ギブアップボタンを描画するためのクラス
/// </summary>
public class GiveUpButton : MonoBehaviour
{
    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // ギブアップボタン
    [SerializeField]
    GameObject giveUpButton = default;

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // アタッチされたオブジェクトがアクティブならギブアップボタンを表示する
        if (gameObject.activeInHierarchy)
        {
            giveUpButton.SetActive(true);
        }
    }

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
