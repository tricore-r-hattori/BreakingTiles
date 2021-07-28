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
    RectTransform imageTransform = default;

    // 瓦の画像を変える地点
    [SerializeField]
    RectTransform tileSpriteChangePoint = default;

    [SerializeField]
    Probability probability;

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

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 割れていない状態に戻す地点に到達したら画像を変換
        if (imageTransform.position.y < tileSpriteChangePoint.position.y && tileImage.sprite == breakTileSprite)
        {
            // 割れていない瓦に変換
            tileImage.sprite = tileSprite;
        }

        // 割れている状態に戻す地点に到達したら画像を変換
        if (imageTransform.position.y > tileSpriteChangePoint.position.y && tileImage.sprite == tileSprite)
        {
            // 割れている瓦に変換
            tileImage.sprite = breakTileSprite;
        }

        if (probability.IsRareTile && imageTransform.position.y < tileSpriteChangePoint.position.y /*&& tileImage.sprite == breakRareTileSprite*/)
        {
            tileImage.sprite = rareTileSprite;
        }

        if (probability.IsRareTile && imageTransform.position.y > tileSpriteChangePoint.position.y /*&& tileImage.sprite == rareTileSprite*/)
        {
            tileImage.sprite = breakRareTileSprite;
        }
    }
}
