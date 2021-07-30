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

    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // 初期座標
    Vector3 InitPosition = Vector3.zero;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void OnEnable()
    {
        if (isProcessOnce)
        {
            InitPosition = hand.position;
        }

        base.Init();

        transform.position = InitPosition;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールできる状態だったらスクロール処理を行う
        if (scrollControllObjectHitCheck.State == ScrollState.Scrollable)
        {
            base.UpdateBase();

            // 上方向にスクロール
            hand.position += velocity;

            // 手がスクロール終了地点に到達したら、手を非アクティブにする
            if (hand.position.y >= handScrollEndPoint.position.y)
            {
                //// 手を非アクティブ化
                //gameObject.SetActive(false);
                velocity = Vector3.zero;

                isProcessOnce = false;
            }
        }
    }
}
