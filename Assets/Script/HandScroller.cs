using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手のスクロール処理
/// </summary>
public class HandScroller : BaseScroller
{
    // 手
    [SerializeField]
    RectTransform hand = default;

    // スクロール終了地点
    [SerializeField]
    RectTransform handScrollEndPoint = default;

    // スクロールを開始せるためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollStartObjectHitCheck scrollStartObjectHitCheck = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        base.Init();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールを開始せるためのオブジェクトと当たったらスクロール処理を行う
        if (scrollStartObjectHitCheck.isScrollStartObjectHit)
        {
            base.UpdateBase();

            // 上方向にスクロール
            hand.position += velocity;

            // 手がスクロール終了地点に到達したら、手を非アクティブにする
            if (hand.position.y >= handScrollEndPoint.position.y)
            {
                // 手を非アクティブ化
                gameObject.SetActive(false);
            }
        }
    }
}
