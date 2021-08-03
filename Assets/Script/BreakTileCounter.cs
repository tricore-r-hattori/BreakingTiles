using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 割った瓦をカウント
/// </summary>
public class CountBreakTile : MonoBehaviour
{
    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // 割った瓦を数えるテキスト
    [SerializeField]
    GameObject breakTileScoreText = default;

    // 割った瓦をカウントするオブジェクトのTextMeshPro
    [SerializeField]
    TextMeshProUGUI CountText = default;

    // 瓦の画像を変換するスクリプトのリスト
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // 割った瓦をカウント
    int breakTilesCount = 0;

    // カウントする瓦の単位の文字列
    const string breakTileCountUnitString = "枚";

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // カウント初期化
        breakTilesCount = 0;

        // 全ての瓦にカウント表示関数を登録
        for (int i = 0; i < tileImageChanger.Count; i++)
        {
            tileImageChanger[i].InitCountBreakTileAction(CountBreakTileText);
        }

        scrollControllObjectHitCheck.InitShowBreakTileScoreText(ShowBreakTileScoreText);
    }

    /// <summary>
    /// テキストを表示
    /// </summary>
    void ShowBreakTileScoreText()
    {
        breakTileScoreText.SetActive(true);
    }

    /// <summary>
    /// 非アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnDisable()
    {
        // テキストを非表示
        breakTileScoreText.SetActive(false);
    }

    /// <summary>
    /// 割った瓦のカウントした値をテキストで表示
    /// </summary>
    void CountBreakTileText()
    {
        breakTilesCount++;
        CountText.text = breakTilesCount + breakTileCountUnitString;
    }
}
