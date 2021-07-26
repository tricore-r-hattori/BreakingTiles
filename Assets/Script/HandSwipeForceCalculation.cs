using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// スワイプ時の力の計算
/// </summary>
public class HandSwipeForceCalculation : MonoBehaviour
{
    // 手
    [SerializeField]
    RectTransform handTransform = default;

    // 瓦
    [SerializeField]
    RectTransform tileTransform = default;

    // 手の座標の補正値
    [SerializeField]
    float correctionHandPositionsY = 0.7f;

    // 瓦の座標の補正値
    [SerializeField]
    float correctionTilePositionsY = 6.6f;

    // 座標を取得するタイミング(フレーム)
    [SerializeField]
    int getPositionTime = 60;

    // 手の座標
    Vector3 handPos = Vector3.zero;
    // 瓦の座標
    Vector3 tilePos = Vector3.zero;

    // スワイプした距離
    float distance = 0.0f;
    // 秒
    float seconds = 0.0f;
    // スワイプ時の力
    // TODO: 瓦との当たり判定をとった後に使用します。
    float swipeForce = 0.0f;
    // フレームのカウント
    int frameCount = 0;

    // フレームから秒に変える値
    const float FrameToSeconds = 60.0f;

    /// <summary>
    /// 速度
    /// </summary>
    public float Speed { get; private set; } = 0.0f;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // 瓦の座標を設定
        tilePos = tileTransform.position;
        // 瓦のY軸を補正
        tilePos.y += correctionTilePositionsY;

        // スワイプした距離を計算
        distance = Mathf.Abs(Vector3.SqrMagnitude(handPos - tilePos));

        // フレームから秒の値に変換
        seconds = (getPositionTime / FrameToSeconds);

        // 加速度を計算
        Speed = distance / (seconds * seconds);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        frameCount++;

        // 1秒ごとに手の座標を取得する
        if (frameCount % getPositionTime == 0)
        {
            // 手の座標を取得
            handPos = handTransform.position;
            // 手のY軸を補正
            handPos.y -= correctionHandPositionsY;

            // フレームのカウントを初期化
            frameCount = 0;
        }
    }

    // 瓦の当たる位置によって割る力を変化させる処理
    // TODO: 瓦の特定部分との当たり判定をとっていないので、後にここの処理を書きます。
    // public void TileIsHitPower()
    // {

    // }
}