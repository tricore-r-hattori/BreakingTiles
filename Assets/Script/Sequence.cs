using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移
/// </summary>
public class Sequence : MonoBehaviour
{
    // スクロール処理を操作するためのオブジェクトと当たったか確認する処理
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // スクロール処理を操作するためのオブジェクト
    [SerializeField]
    GameObject scrollControllObject = default;

    [SerializeField]
    GameObject resultObject = default;

    [SerializeField]
    GameObject gamePlayObject = default;

    // シーケンスのアニメーター
    [SerializeField]
    Animator sequenceAnimator = default;

    // 待つ時間
    [SerializeField]
    float WaitTime = 1.0f;

    // 一回クリックできる状態か
    bool isStateClickableOnce = false;

    // コルーチン指定文字列
    const string coroutineString = "WaitResultSequenceCoroutine";

    // リザルトへ遷移するためのトリガー指定文字列
    const string resultTriggerString = "isResultScene";

    // タイトルへ遷移するためのトリガー指定文字列
    const string titleTriggerString = "isTitleScene";

    /// <summary>
    /// アクティブ化した時に1回だけ処理を行う
    /// </summary>
    void OnEnable()
    {
        // 初期化
        scrollControllObjectHitCheck.Init(WaitResultSequence);
        scrollControllObject.SetActive(true);
        isStateClickableOnce = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (resultObject.activeSelf)
        {
            // マウスクリックを離した、かつ一回クリックできる状態ならタイトルへ遷移する
            if (Input.GetMouseButtonUp(0) && isStateClickableOnce)
            {
                sequenceAnimator.SetTrigger(titleTriggerString);
                isStateClickableOnce = false;
            }
        }
    }

    /// <summary>
    /// 数秒待ってリザルトへ遷移する
    /// </summary>
    void WaitResultSequence()
    {
        if (gamePlayObject.activeSelf)
        {
            StartCoroutine(coroutineString);
        }
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
