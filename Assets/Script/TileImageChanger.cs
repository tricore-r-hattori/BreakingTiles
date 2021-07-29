using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイルの種類
/// </summary>
public enum TileType
{
    Tile,
    RareTile,
}

/// <summary>
/// 瓦の画像を変換する処理
/// </summary>
public class TileImageChanger : MonoBehaviour
{
    /// <summary>
    /// 瓦の画像
    /// </summary>
    [SerializeField]
    Image tileImage = default;

    /// <summary>
    /// 瓦
    /// </summary>
    [SerializeField]
    RectTransform tileTransform = default;

    /// <summary>
    /// 瓦の画像を変える地点
    /// </summary>
    [SerializeField]
    RectTransform tileSpriteChangePoint = default;

    /// <summary>
    /// 割れていない瓦の画像のリスト
    /// </summary>
    [SerializeField]
    List<Sprite> tileSprite = default;

    /// <summary>
    /// 割れている瓦の画像のリスト
    /// </summary>
    [SerializeField]
    List<Sprite> breakTileSprite = default;

    /// <summary>
    /// 確率判定でレア瓦の画像を変更するか確認
    /// </summary>
    [SerializeField]
    RareTileChangeChecker rareTileChangeChecker = default;

    /// <summary>
    /// 瓦が割れたか確認するフラグ
    /// </summary>
    bool isBreakTile = false;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        isBreakTile = false;

        // レア瓦の画像を変更できる状態ならレア瓦の画像に設定
        if (rareTileChangeChecker.IsRareTileChange)
        {
            // レア瓦の画像に設定
            tileImage.sprite = tileSprite[(int)TileType.RareTile];
        }
        // レア瓦の画像を変更できない状態なら通常の瓦の画像に設定
        else
        {
            // 通常の瓦の画像に設定
            tileImage.sprite = tileSprite[(int)TileType.Tile];
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // レア瓦に変更できる状態
        if (rareTileChangeChecker.IsRareTileChange)
        {
            // 画像を変える地点より低い位置にいた時かつ割れているレア瓦の状態の時に画像を変換
            if (tileTransform.position.y < tileSpriteChangePoint.position.y && isBreakTile)
            {
                // 割れていないレア瓦に変換
                tileImage.sprite = tileSprite[(int)TileType.RareTile];
                isBreakTile = false;
            }

            // 画像を変える地点より高い位置にいた時かつ割れていないレア瓦の状態の時に画像を変換
            if (tileTransform.position.y > tileSpriteChangePoint.position.y && !isBreakTile)
            {
                // 割れているレア瓦に変換
                tileImage.sprite = breakTileSprite[(int)TileType.RareTile];
                isBreakTile = true;
            }
        }
        // レア瓦に変更できない状態
        else
        {
            // 画像を変える地点より低い位置にいた時かつ割れている瓦の状態の時に画像を変換
            if (tileTransform.position.y < tileSpriteChangePoint.position.y && isBreakTile)
            {
                // 割れていない瓦に変換
                tileImage.sprite = tileSprite[(int)TileType.Tile];
                isBreakTile = false;
            }

            // 画像を変える地点より高い位置にいた時かつ割れていない瓦の状態の時に画像を変換
            if (tileTransform.position.y > tileSpriteChangePoint.position.y && !isBreakTile)
            {
                // 割れている瓦に変換
                tileImage.sprite = breakTileSprite[(int)TileType.Tile];
                isBreakTile = true;
            }
        }
    }
}
