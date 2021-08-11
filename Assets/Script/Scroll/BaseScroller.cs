using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロール処理
/// </summary>
public class BaseScroller : MonoBehaviour
{
    // 手をスワイプした時の速度
    [SerializeField]
    HandSwipeSpeedController handSwipeSpeed = default;

    // スクロール速度Y軸
    [SerializeField]
    float scrollSpeedY = 100.0f;

    // スクロール速度の減速値
    [SerializeField]
    float speedDeceleration = 0.99f;

    // スクロールを止めるための値
    [SerializeField]
    float scrollStop = 0.01f;

    // スクロール速度
    protected Vector3 velocity = Vector3.zero;

    // 処理を一回だけ行うためのフラグ
    protected bool isProcessOnce = false;

    /// <summary>
    /// スクロールがストップしたかのフラグ
    /// </summary>
    public bool IsScrollStop { get; private set; } = false;

    /// <summary>
    /// スクロール初期化処理
    /// </summary>
    protected void Init()
    {
        isProcessOnce = true;

        IsScrollStop = false;

        velocity = Vector3.zero;
    }

    /// <summary>
    /// スクロール更新処理
    /// </summary>
    protected void UpdateBase()
    {
        // 1回だけスクロール速度設定処理を行う
        if (isProcessOnce)
        {
            // スクロール速度設定
            velocity = new Vector3(0, Time.deltaTime * scrollSpeedY);
            // スクロール速度にスワイプ時の速度を掛ける
            velocity *= handSwipeSpeed.Speed;

            isProcessOnce = false;
        }

        // 減速
        velocity *= speedDeceleration;

        // スクロールを止める
        if (velocity.y <= scrollStop)
        {
            IsScrollStop = true;
            velocity.y = 0.0f;
        }
    }
}
