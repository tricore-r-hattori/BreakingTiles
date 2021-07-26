using UnityEngine;
using System.Collections;

/// <summary>
/// アタッチされているオブジェクトの座標とマウスの座標を同期するためのクラス
/// </summary>
public class PositionSynchronize : MonoBehaviour
{
    // 当たり判定用のオブジェクトと当たったか確認する
    [SerializeField]
    HitCheck hitCheck = default;

    // マウスの座標の補正値
    [SerializeField]
    float correctionHandPositionsZ = 10.0f;

    // マウスのスクリーン座標
    Vector3 mouseScreenPosition = Vector3.zero;
    // スクリーン座標をワールド座標に変換したマウスの座標
    Vector3 screenToWorldMousePosition = Vector3.zero;
    // マウスを左クリックした際にプレイヤーと当たったかどうかのフラグ
    bool isHitPlayer = false;

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 当たり判定用のオブジェクトと当たっていなかったら、マウスとの当たり判定処理を行わないようにする
        if (!hitCheck.isHit)
        {
            // マウスの左ボタンを押したら
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                isHitPlayer = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            }

            // 対象のオブジェクトと当たったらかつマウスの左ボタンが押されている時にマウスの座標と同期する
            if (isHitPlayer && Input.GetMouseButton(0))
            {
                // アタッチされているオブジェクトの座標をマウスの座標に設定
                SetObjectPos();
            }
        }
    }

    /// <summary>
    /// アタッチされているオブジェクトの座標をマウスの座標に設定する処理
    /// </summary>
    void SetObjectPos()
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