using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移
/// </summary>
public class SequenceController : MonoBehaviour
{
    // スクロール処理を操作するためのオブジェクトと当たったか確認する処理
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // スクロール処理を操作するためのオブジェクト
    [SerializeField]
    GameObject scrollControllObject = default;

    // リザルトの全オブジェクトをまとめたオブジェクト
    [SerializeField]
    GameObject resultObject = default;

    // ゲーム中の全オブジェクトをまとめたオブジェクト
    [SerializeField]
    GameObject gamePlayObject = default;

    // チュートリアルの全オブジェクトをまとめたオブジェクト
    [SerializeField]
    GameObject tutorialObject = default;

    // シーケンスのアニメーター
    [SerializeField]
    Animator sequenceAnimator = default;

    // 待つ時間
    [SerializeField]
    float WaitTime = 1.0f;

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
        scrollControllObjectHitCheck.InitResultSequenceAction(ResultSequenceCallCoroutine);
        scrollControllObject.SetActive(true);

        // リザルトオブジェクトまたは、チュートリアルオブジェクトがアクティブだったら入力待ち処理を行う
        if (resultObject.activeSelf || tutorialObject.activeSelf)
        {
            StartCoroutine(WaitTitleSequence());
        }
    }

    /// <summary>
    /// マウスクリックを離すまで待ち、マウスクリックを離したらタイトルへ遷移する
    /// </summary>
    /// <returns>入力されるまで待った</returns>
    IEnumerator WaitTitleSequence()
    {
        // マウスクリックを離した時、タイトルへ遷移する
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        sequenceAnimator.SetTrigger(titleTriggerString);
    }

    /// <summary>
    /// リザルトへ遷移する時のコルーチンを呼ぶ関数
    /// </summary>
    void ResultSequenceCallCoroutine()
    {
        if (gamePlayObject.activeSelf)
        {
            StartCoroutine(WaitResultSequence());
        }
    }

    /// <summary>
    /// リザルトへ遷移するために数秒待つ
    /// </summary>
    /// <returns>数秒待った</returns>
    IEnumerator WaitResultSequence()
    {
        yield return new WaitForSeconds(WaitTime);
        sequenceAnimator.SetTrigger(resultTriggerString);
    }
}
