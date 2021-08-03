using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 割った瓦をカウント
/// </summary>
public class CountBrokenTile : MonoBehaviour
{
    // 割った瓦をカウントするオブジェクトのTextMeshPro
    [SerializeField]
    TextMeshProUGUI CountText = default;

    // 瓦の画像を変換するスクリプトのリスト
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // 割った瓦をカウント
    int brokenTilesCount = 0;

    // カウントする瓦のテキストの文字列
    string countBrokenTileString = default;

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // 初期化
        brokenTilesCount = 0;

        // 全ての瓦に関数を登録
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].Init(CountBrokenTileText);
        }
    }

    /// <summary>
    /// 割った瓦をカウントした値をテキストで表示
    /// </summary>
    public void CountBrokenTileText()
    {
        brokenTilesCount++;
        countBrokenTileString = brokenTilesCount.ToString() + "枚";
        CountText.text = countBrokenTileString;
    }
}
