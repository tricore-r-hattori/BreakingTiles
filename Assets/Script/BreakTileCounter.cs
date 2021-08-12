using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 鳴らすタイミングの種類
/// </summary>
public enum PlayTimingType
{
    Normal,
    Fast,
}

/// <summary>
/// 割った瓦をカウント
/// </summary>
public class BreakTileCounter : MonoBehaviour
{
    // 瓦の画像を変換するスクリプトのリスト
    [SerializeField]
    List<TileImageChanger> tileImageChanger = default;

    // 割る時の音を鳴らすタイミング
    [SerializeField]
    List<float> breakTileSETiming = default;

    // 速度のしきい値
    [SerializeField]
    List<float> speedThreshold = default;

    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // 割った瓦の値を表示するテキスト(TextMeshPro)
    [SerializeField]
    TextMeshProUGUI breakTileScoreText = default;

    // 音を操作
    [SerializeField]
    SoundController soundController = default;

    // 瓦のスクロール処理
    [SerializeField]
    TileScroller tileScroller = default;

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
    /// 非アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnDisable()
    {
        // テキストを非表示
        breakTileScoreText.enabled = false;
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
    /// 割った瓦をカウント
    /// </summary>
    void CountBreakTileText()
    {
        BreakTilesCount++;
        breakTileScoreText.text = BreakTilesCount + BreakTileCountUnitString;

        // タイミングよく割った音を再生する処理
        TimingPlaySE();
    }

    /// <summary>
    /// タイミングよく割った音を再生する処理
    /// </summary>
    void TimingPlaySE()
    {
        // 速度が一定以上に達したら
        if (tileScroller.GetVelocity.y >= speedThreshold[(int)PlayTimingType.Fastest])
        {
            // 任意のタイミングで音を割る鳴らす
            if (BreakTilesCount % breakTileSETiming[(int)PlayTimingType.Fastest] == 0)
            {
                soundController.PlaySE(SoundController.SEType.BreakTileSE);
            }
        }
        // 速度が一定以上に達したら
        else if (tileScroller.GetVelocity.y >= speedThreshold[(int)PlayTimingType.Fast])
        {
            // 任意のタイミングで割る音を鳴らす
            if (BreakTilesCount % breakTileSETiming[(int)PlayTimingType.Fast] == 0)
            {
                soundController.PlaySE(SoundController.SEType.BreakTileSE);
            }
        }
        // 速度が一定以上達していなかったら割る音を鳴らす
        else
        {
            soundController.PlaySE(SoundController.SEType.BreakTileSE);
        }
    }
}
