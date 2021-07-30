using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ギブアップボタンを表示する
/// </summary>
public class ShowGiveUpButton : MonoBehaviour
{
    // ギブアップボタン
    [SerializeField]
    GameObject giveUpButton = default;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // アタッチされたオブジェクトがアクティブならギブアップボタンを表示する
        if (gameObject.activeInHierarchy)
        {
            giveUpButton.SetActive(true);
        }
    }
}
