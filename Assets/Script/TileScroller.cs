using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓦のスクロール処理
/// </summary>
public class TileScroller : BaseScroller
{
    // 瓦
    [SerializeField]
    RectTransform tile = default;

    // スクロール開始地点
    [SerializeField]
    RectTransform tileScrollStartPoint = default;

    // スクロール終了地点
    [SerializeField]
    RectTransform tileScrollEndPoint = default;

    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // スクロール開始座標
    Vector3 movePoint = Vector3.zero;

    // スクロール時のズレを補正するための変数
    float correctionPosition = 0.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // スクロール初期化処理
        base.Init();

        // スクロール開始座標設定
        movePoint = tileScrollStartPoint.position;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールを操作するためのオブジェクトと当たったらスクロール処理を行う
        if (scrollControllObjectHitCheck.IsScrollControllObjectHit)
        {
            // スクロール更新処理
            base.UpdateBase();

            // 上方向にスクロール
            tile.position += velocity;

            // 瓦がスクロール終了地点に到達したら、スクロール開始地点に戻す処理
            if (tile.position.y >= tileScrollEndPoint.position.y)
            {
                // スクロール終了地点から瓦のローカル座標を引いて、ズレた差分を求める
                correctionPosition = tile.position.y - tileScrollEndPoint.position.y;
                // スクロールのズレを補正
                movePoint.y += correctionPosition;
                // 瓦をスクロール開始地点に戻す
                tile.position = movePoint;
                // スクロール開始地点を初期化
                movePoint.y = tileScrollStartPoint.position.y;
            }
        }
    }
}