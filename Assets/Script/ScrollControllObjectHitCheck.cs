using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロール処理を操作するためのオブジェクトと当たったか確認する処理
/// </summary>
public class ScrollControllObjectHitCheck : MonoBehaviour
{
    // 瓦のスクロール
    [SerializeField]
    TileScroller tileScroller = default;

    /// <summary>
    /// スクロール処理を操作するためのオブジェクトと当たったかのフラグ
    /// </summary>
    public bool IsScrollControllObjectHit { get; private set; } = false;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 手と当たったら
        if (collision.tag == "Hand")
        {
            // スクロール処理を操作するためのフラグをtrueにしスクロールを開始する
            IsScrollControllObjectHit = true;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 瓦のスクロールが止まったら、それぞれの処理を止めるためにフラグを切り替える
        if (tileScroller.IsScrollStop)
        {
            // スクロール処理を操作するためのフラグをfalseにしスクロールを終了する
            IsScrollControllObjectHit = false;
        }
    }
}
