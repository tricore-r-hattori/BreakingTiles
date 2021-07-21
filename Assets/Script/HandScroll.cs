using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手のスクロール処理
/// </summary>
public class HandScroll : MonoBehaviour
{
    // 手
    [SerializeField]
    RectTransform hand = default;

    // 手をスワイプした時の力
    [SerializeField]
    HandSwipeForceCalculation handSwipeForce = default;

    // スクロール速度Y軸
    [SerializeField]
    float scrollSpeedY = 100.0f;

    // スクロール速度の減速値
    [SerializeField]
    float speedDeceleration = 0.99f;

    // スクロールを止めるための値
    [SerializeField]
    float scrollStop = 0.5f;

    // スクロール速度
    Vector3 velocity = Vector3.zero;

    // スクロール終了地点のY軸
    const float ScrollEndPointY = 500.0f;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // スクロール速度設定
        velocity = new Vector3(0, Time.deltaTime * scrollSpeedY);
        // スクロール速度にスワイプ時の力を掛ける
        velocity *= handSwipeForce.GetSpeed;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 上方向にスクロール
        hand.localPosition += velocity;

        // 減速
        velocity *= speedDeceleration;

        // スクロールを止める
        if (velocity.y <= scrollStop)
        {
            velocity.y = 0.0f;
        }

        // 手がスクロール終了地点に到達したら、手を消去する処理
        if (hand.localPosition.y >= ScrollEndPointY)
        {
            // 手を消去
            Destroy(gameObject);
        }
    }
}