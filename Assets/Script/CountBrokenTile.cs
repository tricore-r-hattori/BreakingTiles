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
    TextMeshProUGUI countText = default;

    // 瓦の画像を変換するスクリプトのリスト
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // 割った瓦をカウント
    int brokenTilesCount = 0;
    // 割ったレア瓦をカウント
    int breakRareTilesCount = 0;

    // カウントする瓦のテキストの文字列
    public string CountBrokenTileString { get; private set; } = default;

    // 確率判定でレア瓦の画像を変更するか確認
    [SerializeField]
    RareTileChangeChecker rareTileChangeChecker = default;

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
            tileImageChanger[i].Init(CountBrokenTileText);
        }
    }

    /// <summary>
    /// 割った瓦のカウントした値をテキストで表示
    /// </summary>
    void CountBrokenTileText()
    {
        if (rareTileChangeChecker.IsRareTileChange)
        {
            breakRareTilesCount++;
            CountBrokenTileString = breakRareTilesCount + "枚";
        }
        else
        {
            brokenTilesCount++;
            CountBrokenTileString = brokenTilesCount + "枚";
        }

        countText.text = CountBrokenTileString;
    }
}
