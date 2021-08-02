using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移
/// </summary>
public class ResultTransition : MonoBehaviour
{
    // スクロール処理を操作するためのオブジェクトと当たったか確認する処理
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // スクロール処理を操作するためのオブジェクト
    [SerializeField]
    GameObject scrollControllObject = default;

    // シーケンスのアニメーター
    [SerializeField]
    Animator sequenceAnimator = default;

    // 待つ時間
    [SerializeField]
    float WaitTime = 1.0f;

    // コルーチン指定文字列
    string coroutineString = "WaitResultSequenceCoroutine";

    // リザルトへ遷移するためのトリガー指定文字列
    string resultTriggerString = "isResultScene";

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // 初期化
        scrollControllObjectHitCheck.Init(WaitResultSequence);
        scrollControllObject.SetActive(true);
    }

    /// <summary>
    /// 数秒待ってリザルトへ遷移する
    /// </summary>
    void WaitResultSequence()
    {
        StartCoroutine(coroutineString);
    }

    /// <summary>
    /// リザルトへ遷移するために数秒待つコルーチン
    /// </summary>
    /// <returns>数秒待つ</returns>
    IEnumerator WaitResultSequenceCoroutine()
    {
        yield return new WaitForSeconds(WaitTime);
        sequenceAnimator.SetTrigger(resultTriggerString);
    }
}
