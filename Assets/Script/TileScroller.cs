using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓦のスクロール処理
/// </summary>
public class TileScroller : BaseScroller
{
    // 瓦
    [SerializeField]
    RectTransform tile = default;

    // スクロール開始地点
    [SerializeField]
    RectTransform tileScrollStartPoint = default;

    // スクロール終了地点
    [SerializeField]
    RectTransform tileScrollEndPoint = default;

    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // スクロール開始座標
    Vector3 movePoint = Vector3.zero;

    // 初期座標
    Vector3 InitPosition = Vector3.zero;

    // スクロール時のズレを補正するための変数
    float correctionPosition = 0.0f;

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // 起動時に1回だけ処理を行うためのフラグをONにする
        isProcessOnce = true;
    }

    /// <summary>
    /// アクティブ化して1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // 1回だけ初期化する
        if (isProcessOnce)
        {
            // スクロール開始座標設定
            movePoint = tileScrollStartPoint.position;

            // 初期座標設定
            InitPosition = tile.position;
        }

        // スクロール初期化処理
        base.Init();

        // 座標初期化
        tile.position = InitPosition;
    }

    /// <summary>
    /// 非アクティブ化して1回だけ処理を行う
    /// </summary>
    void OnDisable()
    {
        // 起動時に1回だけ処理を行うためのフラグをOFFにする
        isProcessOnce = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールできる状態だったらスクロール処理を行う
        if (scrollControllObjectHitCheck.State == ScrollState.Scrollable)
        {
            // スクロール更新処理
            base.UpdateBase();

            // 上方向にスクロール
            tile.position += velocity;

            // 瓦がスクロール終了地点に到達したら、スクロール開始地点に戻す処理
            if (tile.position.y >= tileScrollEndPoint.position.y)
            {
                // スクロール終了地点から瓦のローカル座標を引いて、ズレた差分を求める
                correctionPosition = tile.position.y - tileScrollEndPoint.position.y;
                // スクロールのズレを補正
                movePoint.y += correctionPosition;
                // 瓦をスクロール開始地点に戻す
                tile.position = movePoint;
                // スクロール開始地点を初期化
                movePoint.y = tileScrollStartPoint.position.y;
            }
        }
    }
}