using UnityEngine;
using System.Collections;

/// <summary>
/// アタッチされているオブジェクトの座標とマウスの座標を同期するためのクラス
/// </summary>
public class PositionSynchronize : MonoBehaviour
{
    // マウスの座標の補正値
    [SerializeField]
    float correctionHandPositionsZ = 10.0f;

    // マウスのスクリーン座標
    Vector3 mouseScreenPosition = Vector3.zero;
    // スクリーン座標をワールド座標に変換したマウスの座標
    Vector3 screenToWorldMousePosition = Vector3.zero;
    // マウスを左クリックした際にプレイヤーと当たったかどうかのフラグ
    bool isHitPlayer = false;
    // 瓦と当たったか
    bool isHitTile = false;

    /// <summary>
    /// 2Dオブジェクト同士が重なった瞬間に呼び出される
    /// </summary>
    /// <param name="other">当たったCollider2Dオブジェクトの情報</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        isHitTile = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // オブジェクトと当たっていなかったら
        if (!isHitTile)
        {
            // マウスの左ボタンを押した時
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                isHitPlayer = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            }

            // アタッチされているオブジェクトの座標をマウスの座標に設定
            SetObjectPos();
        }
    }

    /// <summary>
    /// アタッチされているオブジェクトの座標をマウスの座標に設定する処理
    /// </summary>
    void SetObjectPos()
    {
        // 対象のオブジェクトと当たったか
        if (isHitPlayer)
        {
            // マウスの左ボタンが押されている時
            if (Input.GetMouseButton(0))
            {
                // マウスの座標を取得する
                mouseScreenPosition = Input.mousePosition;
                // マウスのZ軸補正
                mouseScreenPosition.z = correctionHandPositionsZ;
                // マウスの座標をスクリーン座標からワールド座標に変換する
                screenToWorldMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                // ワールド座標に変換されたマウスの座標をアタッチされているオブジェクトの座標に代入
                gameObject.transform.position = screenToWorldMousePosition;
            }
        }
    }
}