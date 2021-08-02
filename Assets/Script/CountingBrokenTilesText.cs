using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// 割った瓦をカウント
/// </summary>
public class CountingBrokenTilesText : MonoBehaviour
{
    // 割った瓦をカウントするオブジェクトのTextMeshPro
    [SerializeField]
    TextMeshProUGUI CountText = default;

    // 瓦の画像を変換するスクリプトのリスト
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // 割った瓦をカウント
    int brokenTilesCount = 0;

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        brokenTilesCount = 0;

        // 全ての瓦に関数を登録
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].BrokenTileCountTextDraw += BrokenTileCountTextDraw;
        }
    }

    /// <summary>
    /// 割った瓦をカウントした値をテキストで描画
    /// </summary>
    public void BrokenTileCountTextDraw()
    {
        brokenTilesCount++;

        CountText.text = brokenTilesCount.ToString() + "枚";
    }

    /// <summary>
    /// 非アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnDisable()
    {
        // 全ての瓦に登録した関数を解除
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].BrokenTileCountTextDraw -= BrokenTileCountTextDraw;
        }
    }
}
