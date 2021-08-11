using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// スワイプ時の速度を操るためのクラス
/// </summary>
public class HandSwipeSpeedController : MonoBehaviour
{
    // 手の接地判定の座標
    [SerializeField]
    RectTransform handHitTransform = default;

    // 瓦の頭の座標
    [SerializeField]
    RectTransform tileHeadTransform = default;

    // 座標を取得するタイミング(フレーム)
    [SerializeField]
    int getPositionTime = 60;

    // 減数倍率
    [SerializeField]
    float attenuationMagnification = 2.0f;

    // 速度倍率
    [SerializeField]
    float speedMagnification = 10.0f;

    // 手の座標
    Vector3 handPos = Vector3.zero;

    // フレームのカウント
    int frameCount = 0;

    // スコアデータリスト
    const string HitTag = "ScrollControllPoint";

    // フレームから秒に変える値
    const float FrameToSeconds = 60.0f;

    /// <summary>
    /// 速度
    /// </summary>
    public float Speed { get; private set; } = 0.0f;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="collision">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // スクロールを開始せるためのオブジェクトと当たったら
        if (collision.tag == HitTag)
        {
            // X軸の距離
            float distanceX = 0.0f;
            // Y軸の距離
            float distanceY = 0.0f;
            // 瓦のX座標を保存
            float saveTilePosX = 0.0f;
            // 秒
            float seconds = 0.0f;
            // 瓦の座標
            Vector3 tilePos = Vector3.zero;

            Speed = 0.0f;
            // 瓦の座標を設定
            tilePos = tileHeadTransform.position;

            saveTilePosX = tilePos.x;
            // 縦の距離だけを計算するために瓦のX軸を手のX軸と同期させる
            tilePos.x = handPos.x;
            // Y軸の距離を計算
            distanceY = Mathf.Abs(Vector3.Distance(handPos, tilePos));

            handPos.y = tilePos.y;
            tilePos.x = saveTilePosX;
            // X軸の距離を計算
            distanceX = Mathf.Abs(Vector3.Distance(handPos, tilePos));

            // フレームから秒の値に変換
            seconds = (getPositionTime / FrameToSeconds);

            // 速度を計算
            Speed = distanceY - (distanceX * attenuationMagnification) / seconds;

            // 速度に任意の倍率を掛ける
            Speed *= speedMagnification;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        frameCount++;

        // 数秒ごとに手の座標を取得する
        if (frameCount % getPositionTime == 0)
        {
            // 手の座標を取得
            handPos = handHitTransform.position;

            // フレームのカウントを初期化
            frameCount = 0;
        }
    }
}