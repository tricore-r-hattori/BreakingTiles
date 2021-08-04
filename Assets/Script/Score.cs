using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// スコアの種類
/// </summary>
public enum ScoreType
{
    PastScore,
    TileHighScore,
    RareTileHighScore,
    NowScore,
}

/// <summary>
/// スコア表示
/// </summary>
public class Score : MonoBehaviour
{
    // 割った瓦をカウント
    [SerializeField]
    BreakTileCounter breakTileCounter = default;

    // スコアを表示するテキスト(TextMeshPro)
    [SerializeField]
    List<TextMeshProUGUI> ScoreText = default;

    // 確率判定でレア瓦の画像を変更するか確認
    [SerializeField]
    RareTileChangeChecker rareTileChangeChecker = default;

    // 現在のスコアリスト
    List<int> nowScoreList = new List<int> { 0,0,0 };

    // スコアデータリスト
    List<string> scoreDateList = new List<string> { "PastScoreData", "TileHighScoreData", "RareTileHighScoreData" };

    // カウントする瓦の単位の文字列
    const string breakTileCountUnitString = "枚";

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // スコアデータを読み込み
        for (int i = 0; i < scoreDateList.Count; i++)
        {
            nowScoreList[i] = PlayerPrefs.GetInt(scoreDateList[i], 0);
        }
    }

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // 現在のスコアと過去のハイスコアを比べて、現在のスコアの値の方が大きかったらハイスコア更新(通常の瓦)
        if (breakTileCounter.BreakTilesCount >= nowScoreList[(int)ScoreType.TileHighScore])
        {
            nowScoreList[(int)ScoreType.TileHighScore] = breakTileCounter.BreakTilesCount;
        }
        // 現在のスコアと過去のハイスコアを比べて、現在のスコアの値の方が大きかったらハイスコア更新(レア瓦)
        if (breakTileCounter.BreakRareTilesCount >= nowScoreList[(int)ScoreType.RareTileHighScore])
        {
            nowScoreList[(int)ScoreType.RareTileHighScore] = breakTileCounter.BreakRareTilesCount;
        }

        // 今までプレイしてきたスコアの合計を計算
        nowScoreList[(int)ScoreType.PastScore] += breakTileCounter.BreakTilesCount + breakTileCounter.BreakRareTilesCount;

        // レアの瓦の状態だったら割ったレア瓦カウントをテキストで表示
        if (rareTileChangeChecker.IsRareTileChange)
        {
            ScoreText[(int)ScoreType.NowScore].text = breakTileCounter.BreakRareTilesCount + breakTileCountUnitString;
        }
        // 通常の瓦の状態だったら割った通常の瓦カウントをテキストで表示
        else
        {
            ScoreText[(int)ScoreType.NowScore].text = breakTileCounter.BreakTilesCount + breakTileCountUnitString;
        }

        // スコアデータを表示して保存する
        for (int i = 0; i < scoreDateList.Count; i++)
        {
            ScoreText[i].text = nowScoreList[i] + breakTileCountUnitString;
            PlayerPrefs.SetInt(scoreDateList[i], nowScoreList[i]);
        }

        PlayerPrefs.Save();
    }
}
