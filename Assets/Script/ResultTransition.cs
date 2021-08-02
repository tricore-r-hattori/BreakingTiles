using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトへのシーン遷移
/// </summary>
public class ResultTransition : MonoBehaviour
{
    // 瓦のスクロール
    [SerializeField]
    TileScroller tileScroller = default;

    // シーケンスのアニメーター
    [SerializeField]
    Animator sequenceAnimator = default;

    // スクロール処理を操作するためのオブジェクトと当たったか確認する処理
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // スクロール処理を操作するためのオブジェクト
    [SerializeField]
    GameObject scrollControllObject = default;

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        scrollControllObject.SetActive(true);
        scrollControllObjectHitCheck.Init(WaitResultSequence);
    }

    /// <summary>
    /// 数秒待ってリザルトへ遷移する
    /// </summary>
    void WaitResultSequence()
    {
        StartCoroutine("WaitResultSequenceCoroutine");
    }

    /// <summary>
    /// リザルトへ遷移するために数秒待つコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitResultSequenceCoroutine()
    {
        yield return new WaitForSeconds(1);
        sequenceAnimator.SetTrigger("isResultScene");
    }
}
