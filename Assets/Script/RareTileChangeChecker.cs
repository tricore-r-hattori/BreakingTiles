using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 確率判定でレア瓦の画像を変更するか確認するためのクラス
/// </summary>
public class RareTileChangeChecker : MonoBehaviour
{
    // 確率
    [SerializeField]
    [Range(0.0f,100.0f)]
    float percent = 100.0f;

    // 100パーセント
    const float OneHundredPercent = 100.0f;

    /// <summary>
    /// レア瓦の画像を変更するかのフラグ
    /// </summary>
    public bool IsRareTileChange { get; private set; } = false;
    
    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // レア瓦の画像を変更するかのフラグに確率判定結果を保存する
        IsRareTileChange = IsCreateRareTileProbability(percent);
    }

    /// <summary>
    /// 確率判定
    /// </summary>
    /// <param name="_percent">確率 (0～100)</param>
    /// <returns>当選結果 [true]当選,[false]落選</returns>
    bool IsCreateRareTileProbability(float _percent)
    {
        // 乱数を計算
        float probabilityRate = Random.value * OneHundredPercent;

        // 確率が100%だったら当選する
        if (_percent == OneHundredPercent)
        {
            return true;
        }
        // 乱数の値が確率の値以下だったら当選する
        else if (probabilityRate <= _percent)
        {
            return true;
        }
        // 上記の条件以外だったら落選
        else
        {
            return false;
        }
    }
}
