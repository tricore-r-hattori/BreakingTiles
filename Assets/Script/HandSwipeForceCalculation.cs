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

    // 座標を取得するタイミング(フレーム)
    [SerializeField]
    int getPositionTime = 60;

    // 手の座標
    Vector3 handPos = Vector3.zero;
    // 瓦の座標
    Vector3 tilePos = Vector3.zero;

    // スワイプした距離
    float distance = 0.0f;
    // 速度
    float speed = 0.0f;
    // 秒
    float seconds = 0.0f;
    // スワイプ時の力
    // TODO: 瓦との当たり判定をとった後に使用します。
    float swipeForce = 0.0f;
    // フレームのカウント
    int frameCount = 0;

    // 手の座標の補正値
    const float correctionHandPositionsY = 1.3f;
    // 瓦の座標の補正値
    const float correctionTilePositionsY = 0.7f;
    // フレームから秒に変える値
    const float frameToSeconds = 60.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // 瓦の座標を仮に設定
        tilePos = tileTransform.position;
        // 瓦のY軸補正
        tilePos.y += correctionTilePositionsY;
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
            // 手のY軸補正
            handPos.y -= correctionHandPositionsY;

            // フレームのカウントを初期化
            frameCount = 0;
        }

        // マウスの左ボタンを離した時
        // NOTE: 瓦との当たり判定を取っていないので、ここの条件式は一旦仮でマウスの左ボタンを離した時に処理を行うようにしています。
        //       本来は瓦と当たった時のフラグを条件式にします。
        if (Input.GetMouseButtonUp(0))
        {
            // スワイプした距離を計算
            distance = Mathf.Abs(Vector3.SqrMagnitude(handPos - tilePos));

            // フレームから秒の値に変換
            seconds = (getPositionTime / frameToSeconds);

            // 速度を計算
            speed = distance / (seconds * seconds);
        }
    }

    // 瓦の当たる位置によって割る力を変化させる処理
    // TODO: 瓦との当たり判定をとっていないので瓦を割るタスクを行う際にここの処理を書きます。
    // public void TileIsHitPower()
    // {

    // }
}