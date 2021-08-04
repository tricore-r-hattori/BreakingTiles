using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロールの状態
/// </summary>
public enum ScrollState
{
    // スクロールできる状態
    Scrollable,
    // スクロールできない状態
    UnScrolling,
}

/// <summary>
/// スクロール処理を操作するためのオブジェクトと当たったか確認する処理
/// </summary>
public class ScrollControllObjectHitCheck : MonoBehaviour
{
    // 瓦のスクロール
    [SerializeField]
    TileScroller tileScroller = default;

    // 手のタグ
    string handTag = "Hand";

    /// <summary>
    /// スクロールが止まって非アクティブになった時に呼ばれるリザルト遷移Action
    /// </summary>
    Action onScrollStopResultSequence = default;

    /// <summary>
    /// スクロールできる状態の時に呼ばれるテキスト表示Action
    /// </summary>
    Action onShowBreakTileScoreText = default;

    /// <summary>
    /// スクロールの状態
    /// </summary>
    public ScrollState State { get; private set; } = ScrollState.UnScrolling;

    /// <summary>
    /// リザルト遷移Action初期化処理
    /// </summary>
    /// <param name="_onScrollStopResultSequence">スクロールが止まって非アクティブになった時に呼ばれるリザルト遷移Action</param>
    public void InitResultSequenceAction(Action _onScrollStopResultSequence)
    {
        this.onScrollStopResultSequence = _onScrollStopResultSequence;
    }

    /// <summary>
    /// テキスト表示Action初期化処理
    /// </summary>
    /// <param name="_onShowBreakTileScoreText">スクロールできる状態の時に呼ばれるテキスト表示Action</param>
    public void InitShowBreakTileScoreText(Action _onShowBreakTileScoreText)
    {
        this.onShowBreakTileScoreText = _onShowBreakTileScoreText;
    }

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D _collision)
    {
        // 手と当たったら
        if (_collision.tag == handTag)
        {
            // スクロールできる状態にして、スクロール処理を開始する
            State = ScrollState.Scrollable;

            // スクロールできる状態の時に呼ばれるテキスト表示Action
            onShowBreakTileScoreText();
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 瓦のスクロールが止まったら、それぞれの処理を止めるためにステートを切り替える
        if (tileScroller.IsScrollStop)
        {
            // スクロールできない状態にして、スクロール処理を終了する
            State = ScrollState.UnScrolling;

            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 非アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnDisable()
    {
        // スクロールが止まって非アクティブになった時に呼ばれるリザルト遷移Action
        onScrollStopResultSequence();
    }
}
