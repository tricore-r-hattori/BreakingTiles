using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 瓦の画像を変換する処理
/// </summary>
public class TileChangeImage : MonoBehaviour
{
    // 瓦の画像
    [SerializeField]
    Image tileImage = default;

    // 瓦
    [SerializeField]
    RectTransform imageTransform = default;
    
    // 割れていない瓦の画像
    [SerializeField]
    Sprite tileSprite = default;

    // 割れている瓦の画像
    [SerializeField]
    Sprite breakTileSprite = default;

    // 割れていない状態に戻す地点のY軸
    const float TileSpriteChangePointY = 1.0f;
    // 割れている状態に戻す地点Y軸
    const float BreakTileSpriteChangePointY = -1.0f;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 割れていない状態に戻す地点に到達したら画像を変換
        if (imageTransform.position.y <= TileSpriteChangePointY)
        {
            // 割れていない瓦に変換
            tileImage.sprite = tileSprite;
        }

        // 割れている状態に戻す地点に到達したら画像を変換
        if (imageTransform.position.y >= BreakTileSpriteChangePointY)
        {
            // 割れている瓦に変換
            tileImage.sprite = breakTileSprite;
        }
    }
}
