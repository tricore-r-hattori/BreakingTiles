using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// スコア
/// </summary>
public class ScoreText : MonoBehaviour
{
    /// <summary>
    /// スコアの種類
    /// </summary>
    public enum ScoreType
    {
        PastScore,
        TileHighScore,
        RareTileHighScore,
        NowScore,
        TitleName,
    }

    /// <summary>
    /// 称号の種類
    /// TODO: 3つだと寂しいので後程増やすかもしれません。 
    /// </summary>
    public enum TitleType
    {
        Premium,
        Special,
        Regular,
    }

    // スコアを表示するテキスト(TextMeshPro)
    [SerializeField]
    List<TextMeshProUGUI> scoreTextList = default;

    // 割った瓦をカウント
    [SerializeField]
    BreakTileCounter breakTileCounter = default;

    // 確率判定でレア瓦の画像を変更するか確認
    [SerializeField]
    RareTileChangeChecker rareTileChangeChecker = default;

    // しきい値のリスト
    [SerializeField]
    List<int> nowScoreThresholdList = default;

    // しきい値のリスト
    [SerializeField]
    List<int> pastScoreThresholdList = default;

    // 現在のスコアのリスト
    List<int> scoreList = new List<int> {0,0,0};

    // スコアデータリスト
    List<string> scoreDateNameList = new List<string> {"PastScoreData", "TileHighScoreData", "RareTileHighScoreData"};

    // 現在のスコアによる称号リスト
    // TODO: 3つだと寂しいので後程増やすかもしれません。
    List<string> nowScoreTitleList = new List<string> {"狼","鬼","神"};

    // 過去のスコアによる称号リスト
    // TODO: 3つだと寂しいので後程増やすかもしれません。
    List<string> pastScoreTitleList = new List<string> {"白帯の","黄帯の","黒帯の"};

    // 現在のスコアによる称号
    string nowScoreTitle = default;

    // 過去のスコアによる称号
    string pastScoreTitle = default;

    // カウントする瓦の単位の文字列
    const string breakTileCountUnitString = "枚";

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // スコアデータを読み込み
        for (int i = 0; i < scoreDateNameList.Count; i++)
        {
            scoreList[i] = PlayerPrefs.GetInt(scoreDateNameList[i], 0);
        }
    }

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // レアの瓦の状態でスコア比較処理へ
        if (rareTileChangeChecker.IsRareTileChange)
        {
            // 現在のスコアと過去のハイスコアを比べて、現在のスコアの値の方が大きかったらハイスコア更新
            if (breakTileCounter.BreakTilesCount >= scoreList[(int)ScoreType.RareTileHighScore])
            {
                scoreList[(int)ScoreType.RareTileHighScore] = breakTileCounter.BreakTilesCount;
            }
        }
        // 通常の瓦の状態でスコア比較処理へ
        else
        {
            // 現在のスコアと過去のハイスコアを比べて、現在のスコアの値の方が大きかったらハイスコア更新
            if (breakTileCounter.BreakTilesCount >= scoreList[(int)ScoreType.TileHighScore])
            {
                scoreList[(int)ScoreType.TileHighScore] = breakTileCounter.BreakTilesCount;
            }
        }

        // 今までプレイしてきたスコアの合計を計算
        scoreList[(int)ScoreType.PastScore] += breakTileCounter.BreakTilesCount;

        // 今割った瓦のカウントをテキストで表示
        scoreTextList[(int)ScoreType.NowScore].text = breakTileCounter.BreakTilesCount + breakTileCountUnitString;

        // スコアデータを表示して保存する
        for (int i = 0; i < scoreDateNameList.Count; i++)
        {
            scoreTextList[i].text = scoreList[i] + breakTileCountUnitString;
            PlayerPrefs.SetInt(scoreDateNameList[i], scoreList[i]);
        }

        PlayerPrefs.Save();

        // 称号名決定処理
        TitleDecision();
    }

    /// <summary>
    /// 称号名決定処理 
    /// </summary>
    void TitleDecision()
    {
        // 現在のスコアがしきい値以上だったら称号名を決定する
        if (breakTileCounter.BreakTilesCount >= nowScoreThresholdList[(int)TitleType.Regular])
        {
            nowScoreTitle = nowScoreTitleList[(int)TitleType.Regular];
        }
        else if (breakTileCounter.BreakTilesCount >= nowScoreThresholdList[(int)TitleType.Special])
        {
            nowScoreTitle = nowScoreTitleList[(int)TitleType.Special];
        }
        else if (breakTileCounter.BreakTilesCount >= nowScoreThresholdList[(int)TitleType.Premium])
        {
            nowScoreTitle = nowScoreTitleList[(int)TitleType.Premium];
        }

        // 過去のスコアがしきい値以上だったら称号名を決定する
        if (scoreList[(int)ScoreType.PastScore] >= pastScoreThresholdList[(int)TitleType.Regular])
        {
            pastScoreTitle = pastScoreTitleList[(int)TitleType.Regular];
        }
        else if (scoreList[(int)ScoreType.PastScore] >= pastScoreThresholdList[(int)TitleType.Special])
        {
            pastScoreTitle = pastScoreTitleList[(int)TitleType.Special];
        }
        else if (scoreList[(int)ScoreType.PastScore] >= pastScoreThresholdList[(int)TitleType.Premium])
        {
            pastScoreTitle = pastScoreTitleList[(int)TitleType.Premium];
        }

        // 過去のスコアの称号と現在のスコアの称号を合わせてテキストで表示
        scoreTextList[(int)ScoreType.TitleName].text = pastScoreTitle + nowScoreTitle;
    }
}
