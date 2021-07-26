using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトと当たったか確認する処理
/// </summary>
public class HitCheck : MonoBehaviour
{
    // 瓦のスクロール
    [SerializeField]
    TileScroller tileScroller = default;
    
    /// <summary>
    /// オブジェクトと当たったかのフラグ
    /// </summary>
    public bool isHit { get; private set; } = false;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // オブジェクトと当たった
        isHit = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 瓦のスクロールが止まったら、それぞれの処理を止めるためにフラグを切り替える
        if (tileScroller.isScrollStop)
        {
            isHit = false;
        }
    }
}
