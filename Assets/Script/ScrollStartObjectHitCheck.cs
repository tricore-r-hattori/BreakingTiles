using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクロールを開始せるためのオブジェクトと当たったか確認する処理
/// </summary>
public class ScrollStartObjectHitCheck : MonoBehaviour
{
    // 瓦のスクロール
    [SerializeField]
    TileScroller tileScroller = default;
    
    /// <summary>
    /// スクロールを開始せるためのオブジェクトと当たったかのフラグ
    /// </summary>
    public bool isScrollStartObjectHit { get; private set; } = false;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 手と当たったら
        if (collision.tag == "Hand")
        {
            // スクロールを開始せるためのオブジェクトと当たった
            isScrollStartObjectHit = true;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 瓦のスクロールが止まったら、それぞれの処理を止めるためにフラグを切り替える
        if (tileScroller.isScrollStop)
        {
            isScrollStartObjectHit = false;
        }
    }
}
