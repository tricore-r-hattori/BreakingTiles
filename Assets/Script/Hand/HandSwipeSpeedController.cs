using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// スワイプ速度を制限させる位置の種類
/// </summary>
public enum LimitSpeedPointType
{
    Leftmost,
    Left,
    Right,
    Rightmost,
}

/// <summary>
/// スワイプ速度の種類
/// </summary>
public enum SwipeSpeedType
{
    Small,
    Large
}

/// <summary>
/// レクトトランスフォームの種類
/// </summary>
public enum RectTransformType
{
    HandHit,
    TileHead,
}

/// <summary>
/// スワイプ時の速度を操るためのクラス
/// </summary>
public class HandSwipeSpeedController : MonoBehaviour
{
    // スワイプ速度を制限させる位置
    [SerializeField]
    List<RectTransform> limitSpeedPointList = default;

    // 速度に掛ける値
    [SerializeField]
    List<float> multiplySpeedList = default;

    // オブジェクトの座標を取得するためのレクトトランスフォーム
    [SerializeField]
    List<RectTransform> rectTransformList = default;

    // 速度のしきい値
    [SerializeField]
    float speedThreshold = 50.0f;

    // 座標を取得するタイミング(フレーム)
    [SerializeField]
    int getPositionTime = 60;

    // 手の座標
    Vector3 handPos = Vector3.zero;
    // 瓦の座標
    Vector3 tilePos = Vector3.zero;

    // X軸の距離
    float distanceX = 0.0f;
    // Y軸の距離
    float distanceY = 0.0f;
    // 瓦のX座標を保存
    float saveTilePosX = 0.0f;
    // 秒
    float seconds = 0.0f;
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
            Speed = 0.0f;
            // 瓦の座標を設定
            tilePos = rectTransformList[(int)RectTransformType.TileHead].position;

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
            Speed = (distanceY - distanceX) / seconds;

            // 瓦の当たる位置によって、速度に掛ける値を変える処理
            MultiplySwipeSpeed();
        }
    }

    /// <summary>
    /// 瓦の当たる位置によって、速度に掛ける値を変える処理
    /// </summary>
    void MultiplySwipeSpeed()
    {
        // 瓦の左側と右側に当たったら小さい値を速度に掛ける
        if (rectTransformList[(int)RectTransformType.HandHit].position.x < limitSpeedPointList[(int)LimitSpeedPointType.Leftmost].position.x ||
            rectTransformList[(int)RectTransformType.HandHit].position.x > limitSpeedPointList[(int)LimitSpeedPointType.Rightmost].position.x)
        {
            Speed *= multiplySpeedList[(int)SwipeSpeedType.Small];
        }
        // 瓦の真ん中に当たったら大きい値を速度に掛ける
        else if (rectTransformList[(int)RectTransformType.HandHit].position.x > limitSpeedPointList[(int)LimitSpeedPointType.Left].position.x &&
                 rectTransformList[(int)RectTransformType.HandHit].position.x < limitSpeedPointList[(int)LimitSpeedPointType.Right].position.x)
        {
            if (Speed >= speedThreshold)
            {
                Speed *= multiplySpeedList[(int)SwipeSpeedType.Large];
            }
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
            handPos = rectTransformList[(int)RectTransformType.HandHit].position;

            // フレームのカウントを初期化
            frameCount = 0;
        }
    }
}