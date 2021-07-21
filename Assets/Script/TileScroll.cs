using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓦のスクロール処理
/// </summary>
public class TileScroll : MonoBehaviour
{
    // 瓦
    [SerializeField]
    RectTransform tile = default;

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
    // スクロール開始座標
    Vector3 MovePoint = Vector3.zero;

    // スクロール時のズレを補正するための変数
    float correctionPosition = 0.0f;

    // スクロール開始地点のY軸
    const float ScrollStartPointY = -780.0f;
    // スクロール終了地点のY軸
    const float ScrollEndPointY = 780.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // スクロール開始座標設定
        MovePoint = new Vector3(0.0f, ScrollStartPointY, 0.0f);
    }

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
        tile.localPosition += velocity;

        // 減速
        velocity *= speedDeceleration;

        // スクロールを止める
        if (velocity.y <= scrollStop)
        {
            velocity.y = 0.0f;
        }

        // 瓦がスクロール終了地点に到達したら、スクロール開始地点に戻す処理
        if (tile.localPosition.y >= ScrollEndPointY)
        {
            // スクロール終了地点から瓦のローカル座標を引いて、ズレた差分を求める
            correctionPosition = tile.localPosition.y - ScrollEndPointY;
            // スクロールのズレを補正
            MovePoint.y += correctionPosition;
            // 瓦をスクロール開始地点に戻す
            tile.localPosition = MovePoint;
            // スクロール開始地点を初期化
            MovePoint.y = ScrollStartPointY;
        }
    }
}