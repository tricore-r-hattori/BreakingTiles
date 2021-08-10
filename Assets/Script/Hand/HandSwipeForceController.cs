using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// 瓦の矩形当たり判定の種類
/// </summary>
public enum TileCollisionType
{
    ScrollControllPoint,
    Leftmost,
    Left,
    Middle,
    Right,
    Rightmost,
}

/// <summary>
/// 瓦に当たった時の力の種類
/// </summary>
public enum HitTilePowerType
{
    Small,
    Medium,
    Large
}

/// <summary>
/// スワイプ時の力を操るためのクラス
/// </summary>
public class HandSwipeForceController : MonoBehaviour
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

    // 速度に掛ける値
    [SerializeField]
    List<float> multiplySpeedValueList = default;

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
    // 速度
    float speed = 0.0f;
    // 速度に掛ける値を保存する変数
    float multiplySpeed = 0.0f;
    // フレームのカウント
    int frameCount = 0;

    // スコアデータリスト
    string[] HitTag = { "ScrollControllPoint" ,"LeftmostTile", "LeftTile", "MiddleTile","RightTile", "RightmostTile" };

    // フレームから秒に変える値
    const float FrameToSeconds = 60.0f;

    /// <summary>
    /// スワイプ時の力
    /// </summary>
    public float SwipeForce { get; private set; } = 0.0f;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="collision">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // スクロールを開始せるためのオブジェクトと当たったら
        if (collision.tag == HitTag[(int)TileCollisionType.ScrollControllPoint])
        {
            // 瓦の座標を設定
            tilePos = tileTransform.position;
            // 瓦のY軸を補正
            tilePos.y += correctionTilePositionsY;
            // 縦の距離だけを計算するために瓦のX軸を手のX軸と同期させる
            tilePos.x = handPos.x;

            // スワイプした距離を計算
            distance = Mathf.Abs(Vector3.Distance(handPos, tilePos));

            // フレームから秒の値に変換
            seconds = (getPositionTime / FrameToSeconds);

            // 速度を計算
            speed = distance / seconds;
        }

        // 瓦の当たる位置によって割る力を変化させる処理
        HitTileIsPower(collision);
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
            handPos = handTransform.position;
            // 手のY軸を補正
            handPos.y -= correctionHandPositionsY;

            // フレームのカウントを初期化
            frameCount = 0;
        }
    }

    /// <summary>
    /// 瓦の当たる位置によって割る力を変化させる処理 
    /// </summary>
    /// <param name="collision">当たったCollider2Dオブジェクトの情報</param>
    public void HitTileIsPower(Collider2D collision)
    {
        // 瓦の一番左と一番右の矩形当たり判定に当たったら、速度に掛ける小さい値を設定
        if (collision.tag == HitTag[(int)TileCollisionType.Leftmost] || collision.tag == HitTag[(int)TileCollisionType.Rightmost])
        {
            multiplySpeed = multiplySpeedValueList[(int)HitTilePowerType.Small];
        }

        // 瓦の左から2番目と右から2番目の矩形当たり判定に当たったら、速度に掛ける中くらいの値を設定
        if (collision.tag == HitTag[(int)TileCollisionType.Left] || collision.tag == HitTag[(int)TileCollisionType.Right])
        {
            multiplySpeed = multiplySpeedValueList[(int)HitTilePowerType.Medium];
        }

        // 瓦の真ん中の矩形当たり判定に当たったら、速度に掛ける大きい値を設定
        if (collision.tag == HitTag[(int)TileCollisionType.Middle])
        {
            multiplySpeed = multiplySpeedValueList[(int)HitTilePowerType.Large];
        }

        // スワイプ時の力を計算
        SwipeForce = speed * multiplySpeed;
    }
}