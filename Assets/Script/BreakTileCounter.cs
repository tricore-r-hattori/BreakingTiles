using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 割った瓦をカウント
/// </summary>
public class BreakTileCounter : MonoBehaviour
{
    // 瓦の画像を変換するスクリプトのリスト
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // 割った瓦の値を表示するテキスト(TextMeshPro)
    [SerializeField]
    TextMeshProUGUI breakTileScoreText = default;

    // 音を操作
    [SerializeField]
    SoundController soundController = default;

    // カウントする瓦の単位の文字列
    const string BreakTileCountUnitString = "枚";

    // カウントテキストの初期値文字列
    const string ZeroSheets = "0枚";

    /// <summary>
    /// 割った瓦をカウント
    /// </summary>
    public int BreakTilesCount { get; private set; } = 0;

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // カウント初期化
        BreakTilesCount = 0;

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
        breakTileScoreText.text = ZeroSheets;
        breakTileScoreText.enabled = true;
    }

    /// <summary>
    /// 非アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnDisable()
    {
        // テキストを非表示
        breakTileScoreText.enabled = false;
    }

    /// <summary>
    /// 割った瓦をカウント
    /// </summary>
    void CountBreakTileText()
    {
         BreakTilesCount++;
         breakTileScoreText.text = BreakTilesCount + BreakTileCountUnitString;

          // 瓦が割れる音を流す
         soundController.PlaySE(SoundController.SEType.BreakTileSE);
    }
}
