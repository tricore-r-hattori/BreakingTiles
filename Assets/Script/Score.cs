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
    CountBrokenTile countBrokenTile = default;

    [SerializeField]
    List<TextMeshProUGUI> ScoreText = default;

    // 1プレイ前のスコア
    int pastScore = 0;

    // 現在のスコア
    int nowScore = 0;

    const string scoreData = "ScoreData";
    void OnEnable()
    {
        pastScore = PlayerPrefs.GetInt(scoreData, 0);

        nowScore = 

        // スコアを保存
        PlayerPrefs.SetInt(scoreData, nowScore);
        PlayerPrefs.Save();

        ScoreText[(int)ScoreType.NormalScore].text = countBrokenTile.CountBrokenTileString;
        ScoreText[(int)ScoreType.PastScore].text = countBrokenTile.CountBrokenTileString;
        //if ()
        //{

        //}
        ScoreText[(int)ScoreType.NormalTileHighScore].text = countBrokenTile.CountBrokenTileString;
        ScoreText[(int)ScoreType.RainbowTileHighScore].text = countBrokenTile.CountBrokenTileString;
    }
}
