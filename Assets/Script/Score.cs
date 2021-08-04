using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ScoreType
{
    NormalScore,
    PastScore,
    NormalTileHighScore,
    RainbowTileHighScore,
}

/// <summary>
/// スコア表示
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField]
    BreakTileCounter breakTileCounter = default;

    [SerializeField]
    List<TextMeshProUGUI> ScoreText = default;

    // 確率判定でレア瓦の画像を変更するか確認
    [SerializeField]
    RareTileChangeChecker rareTileChangeChecker = default;

    // 今までのスコアの合計
    int pastScore = 0;

    // 現在のスコア
    int nowNormalTileHighScore = 0;

    int nowRainbowTileHighScore = 0;

    const string PastScoreData = "PastScoreData";

    const string NormalTileHighScoreData = "NormalTileHighScoreData";

    const string RainbowTileHighScore = "RainbowTileHighScore";

    void Awake()
    {
        pastScore = PlayerPrefs.GetInt(PastScoreData, 0);
        nowNormalTileHighScore = PlayerPrefs.GetInt(NormalTileHighScoreData, 0);
        nowRainbowTileHighScore = PlayerPrefs.GetInt(RainbowTileHighScore, 0);
    }

    void OnEnable()
    {
        if (breakTileCounter.BreakTilesCount >= nowNormalTileHighScore)
        {
            nowNormalTileHighScore = breakTileCounter.BreakTilesCount;
        }
        if (breakTileCounter.BreakRareTilesCount >= nowRainbowTileHighScore)
        {
            nowRainbowTileHighScore = breakTileCounter.BreakRareTilesCount;
        }

        pastScore = pastScore + breakTileCounter.BreakTilesCount + breakTileCounter.BreakRareTilesCount;

        if (rareTileChangeChecker.IsRareTileChange)
        {
            ScoreText[(int)ScoreType.NormalScore].text = breakTileCounter.BreakRareTilesCount + "枚";
        }
        else
        {
            ScoreText[(int)ScoreType.NormalScore].text = breakTileCounter.BreakTilesCount + "枚";
        }

        ScoreText[(int)ScoreType.PastScore].text = pastScore + "枚";
        ScoreText[(int)ScoreType.NormalTileHighScore].text = nowNormalTileHighScore + "枚";
        ScoreText[(int)ScoreType.RainbowTileHighScore].text = nowRainbowTileHighScore + "枚";

        // スコアを保存
        PlayerPrefs.SetInt(PastScoreData, pastScore);
        PlayerPrefs.SetInt(NormalTileHighScoreData, nowNormalTileHighScore);
        PlayerPrefs.SetInt(RainbowTileHighScore, nowRainbowTileHighScore);
        PlayerPrefs.Save();
    }
}
