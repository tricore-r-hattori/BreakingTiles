using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 瓦の画像を変換する処理
/// </summary>
public class TileImageChanger : MonoBehaviour
{
    // 瓦の画像
    [SerializeField]
    Image tileImage = default;

    // 瓦
    [SerializeField]
    RectTransform tileTransform = default;

    // 瓦の画像を変える地点
    [SerializeField]
    RectTransform tileSpriteChangePoint = default;

    // 割れていない瓦の画像
    [SerializeField]
    Sprite tileSprite = default;

    // 割れている瓦の画像
    [SerializeField]
    Sprite breakTileSprite = default;

    // 割れていないレア瓦の画像
    [SerializeField]
    Sprite rareTileSprite = default;

    // 割れているレア瓦の画像
    [SerializeField]
    Sprite breakRareTileSprite = default;

    // 確率判定でレア瓦の画像を変更するか確認
    [SerializeField]
    RareTileChangeCheckWithProbability rareTileChangeCheck = default;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        // レア瓦の画像を変更できる状態ならレア瓦の画像に設定
        if (rareTileChangeCheck.IsRareTileChange)
        {
            // レア瓦の画像に設定
            tileImage.sprite = rareTileSprite;
        }
        // レア瓦の画像を変更できない状態なら通常の瓦の画像に設定
        else
        {
            // 通常の瓦の画像に設定
            tileImage.sprite = tileSprite;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 画像を変える地点より低い位置にいた時かつ割れている瓦の状態かつレア瓦に変更できない時に画像を変換
        if (tileTransform.position.y < tileSpriteChangePoint.position.y && tileImage.sprite == breakTileSprite && !rareTileChangeCheck.IsRareTileChange)
        {
            // 割れていない瓦に変換
            tileImage.sprite = tileSprite;
        }

        // 画像を変える地点より高い位置にいた時かつ割れていない瓦の状態かつレア瓦に変更できない時に画像を変換
        if (tileTransform.position.y > tileSpriteChangePoint.position.y && tileImage.sprite == tileSprite && !rareTileChangeCheck.IsRareTileChange)
        {
            // 割れている瓦に変換
            tileImage.sprite = breakTileSprite;
        }

        // 画像を変える地点より低い位置にいた時かつ割れているレア瓦の状態かつレア瓦に変更できる時に画像を変換
        if (tileTransform.position.y < tileSpriteChangePoint.position.y && tileImage.sprite == breakRareTileSprite && rareTileChangeCheck.IsRareTileChange)
        {
            // 割れていないレア瓦に変換
            tileImage.sprite = rareTileSprite;
        }

        // 画像を変える地点より高い位置にいた時かつ割れていないレア瓦の状態かつレア瓦に変更できる時に画像を変換
        if (tileTransform.position.y > tileSpriteChangePoint.position.y && tileImage.sprite == rareTileSprite && rareTileChangeCheck.IsRareTileChange)
        {
            // 割れているレア瓦に変換
            tileImage.sprite = breakRareTileSprite;
        }
    }
}
