using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 割った瓦を数えるテキストを描画するためのクラス
/// </summary>
public class CountingBrokenTileDraw : MonoBehaviour
{
    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // 割った瓦を数えるテキスト
     [SerializeField]
    GameObject countingBrokenTile = default;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールできる状態だったら割った瓦を数えるテキストを表示する
        if (scrollControllObjectHitCheck.State == ScrollState.Scrollable)
        {
            countingBrokenTile.SetActive(true);
        }
    }

    /// <summary>
    /// 非アクティブ化した時に1回だけ処理を行う
    /// </summary>

    void OnDisable()
    {
        countingBrokenTile.SetActive(false);
    }
}
