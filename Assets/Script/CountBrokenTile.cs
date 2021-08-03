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

    // カウントする瓦の単位の文字列
    const string brokenTileCountUnitString = "枚";

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // カウント初期化
        brokenTilesCount = 0;

        // 全ての瓦に関数を登録
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].InitCountBrokenTileAction(CountBrokenTileText);
        }
    }

    /// <summary>
    /// 割った瓦のカウントした値をテキストで表示
    /// </summary>
    void CountBrokenTileText()
    {
        brokenTilesCount++;
        CountText.text = brokenTilesCount + brokenTileCountUnitString;
    }
}
