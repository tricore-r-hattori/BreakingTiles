using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HandSwipeForceCalculation : MonoBehaviour
{
    // 手
    [SerializeField]
    RectTransform handTransform = default;

    // 瓦
    [SerializeField]
    RectTransform tileTransform = default;

    // 手の座標
    Vector3 handPos = new Vector3(0.0f,0.0f,0.0f);
    // 瓦の座標
    Vector3 tilePos = new Vector3(0.0f,0.0f,0.0f);

    // スワイプした距離
    float distance = 0.0f;
    // 速度
    float speed = 0.0f;
    // フレームのカウント
    int frameCount = 0;

    // 座標を取得するタイミング(フレーム)
    const int getPositionTime = 60;

    // 手の座標の補正値
    const float correctionHandPositionsY = 1.3f;
    // 瓦の座標の補正値
    const float correctionTilePositionsY = 0.7f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        tilePos = tileTransform.position;
        tilePos.y += correctionTilePositionsY;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        frameCount++;

        // 1秒ごとに手の座標を取得する
        if (frameCount % 60.0f == 0)
        {
            handPos = handTransform.position;
            handPos.y -= correctionHandPositionsY;

            frameCount = 0;
        }

        // マウスの左ボタンを離した時
        // 瓦との当たり判定を取っていないので、ここの条件式は一旦仮でマウスの左ボタンを離した時に処理を行うようにしています。
        // 本来は瓦と当たった時のフラグを条件式にします。
        if (Input.GetMouseButtonUp(0))
        {
            // スワイプした距離を取得する
            distance = Mathf.Abs(Vector3.Distance(handPos, tilePos));
        }
    }

    // 瓦の当たる位置によって割る力を変化させる処理
    // 瓦との当たり判定をとっていないので瓦を割るタスクを行う際にここの処理を書きます。
    // public void TileIsHitPower()
    // {

    // }
}