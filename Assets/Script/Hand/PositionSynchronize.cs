using UnityEngine;
using System.Collections;

/// <summary>
/// アタッチされているオブジェクトの座標とマウスの座標を同期するためのクラス
/// </summary>
public class PositionSynchronize : MonoBehaviour
{
    // スクロールを操作するためのオブジェクトと当たったか確認する
    [SerializeField]
    ScrollControllObjectHitCheck scrollControllObjectHitCheck = default;

    // 右下にある手の軸を固定する地点
    [SerializeField]
    RectTransform bottomRightFixHandPoint = default;

    // 左上にある手の軸を固定する地点
    [SerializeField]
    RectTransform topLeftFixHandPoint = default;

    // マウスの座標の補正値
    [SerializeField]
    float correctionHandPositionsZ = 10.0f;

    // レイキャストでオブジェクトと当たった情報を格納
    RaycastHit2D hitObject = default;

    // 手の軸を固定する座標
    Vector3 fixHandPosition = Vector3.zero;

    // マウスのスクリーン座標
    Vector3 mouseScreenPosition = Vector3.zero;
    // スクリーン座標をワールド座標に変換したマウスの座標
    Vector3 screenToWorldMousePosition = Vector3.zero;

    // 手のタグ
    const string HandTag = "Hand";

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // スクロールできない状態だったら、マウスとの当たり判定処理を行わないようにする
        if (scrollControllObjectHitCheck.State == ScrollState.UnScrolling)
        {
            // マウスと当たったオブジェクトの確認
            CheckMouseHitObject();
        }
    }

    /// <summary>
    /// マウスと当たったオブジェクトの確認
    /// </summary>
    void CheckMouseHitObject()
    {
        // マウスの左ボタンを押したら
        // TODO: 後に、タッチした時の条件に変更
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hitObject = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
        }

        // マウスの左ボタンが押されている時に当たったオブジェクトが手だったらマウスの座標と同期する
        // TODO: 後に、タッチしている時の条件に変更
        if (Input.GetMouseButton(0))
        {
            // どのオブジェクトにも当たっていなかったら処理を戻す
            if (hitObject.collider == null)
            {
                return;
            }

            if (hitObject.collider.tag == HandTag)
            {
                // アタッチされているオブジェクトの座標をマウスの座標に設定
                // TODO: 後に、アタッチされているオブジェクトの座標をタッチした座標に設定する処理に変更
                SetAttachObjectPositionToMousePosition();
            }
        }
    }

    /// <summary>
    /// アタッチされているオブジェクトの座標をマウスの座標に設定する処理
    /// </summary>
    /// TODO: 後に、アタッチされているオブジェクトの座標をタッチした座標に設定する処理に変更
    void SetAttachObjectPositionToMousePosition()
    {
        // マウスの座標を取得する
        mouseScreenPosition = Input.mousePosition;
        // マウスのZ軸補正
        mouseScreenPosition.z = correctionHandPositionsZ;
        // マウスの座標をスクリーン座標からワールド座標に変換する
        screenToWorldMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        // ワールド座標に変換されたマウスの座標をアタッチされているオブジェクトの座標に代入
        gameObject.transform.position = screenToWorldMousePosition;
        // 手の軸を固定する座標を更新
        fixHandPosition = gameObject.transform.position;

        // 一定の位置で手の軸を固定する
        FixHand();
    }

    /// <summary>
    /// 一定の位置で手の軸を固定する
    /// </summary>
    void FixHand()
    {
        // ゲーム画面の左側で手を固定する処理
        LeftFixHand();

        // ゲーム画面の右側で手を固定する処理
        RightFixHand();

        // ゲーム画面の下側で手を固定する処理
        DownFixHand();

        // アタッチされたオブジェクトの座標の軸を固定する
        gameObject.transform.position = fixHandPosition;
    }

    /// <summary>
    /// ゲーム画面の左側で手を固定する処理
    /// </summary>
    void LeftFixHand()
    {
        // 手のX軸を固定する地点から左に行ったら手のX軸を固定する
        if (gameObject.transform.position.x <= topLeftFixHandPoint.position.x)
        {
            fixHandPosition.x = topLeftFixHandPoint.position.x;
            fixHandPosition.y = gameObject.transform.position.y;

            // 手のY軸を固定する地点から下に行ったら手のY軸を固定する
            if (gameObject.transform.position.y <= bottomRightFixHandPoint.position.y)
            {
                fixHandPosition.y = topLeftFixHandPoint.position.y;
            }
        }
    }

    /// <summary>
    /// ゲーム画面の右側で手を固定する処理
    /// </summary>
    void RightFixHand()
    {
        // 手のX軸を固定する地点から右に行ったら手のX軸を固定する
        if (gameObject.transform.position.x >= bottomRightFixHandPoint.position.x)
        {
            fixHandPosition.x = bottomRightFixHandPoint.position.x;
            fixHandPosition.y = gameObject.transform.position.y;

            // 手のY軸を固定する地点から下に行ったら手のY軸を固定する
            if (gameObject.transform.position.y <= bottomRightFixHandPoint.position.y)
            {
                fixHandPosition.y = bottomRightFixHandPoint.position.y;
            }
        }
    }

    /// <summary>
    /// ゲーム画面の下側で手を固定する処理
    /// </summary>
    void DownFixHand()
    {
        // 手のY軸を固定する地点から下に行ったら手のY軸を固定する
        if (gameObject.transform.position.y <= bottomRightFixHandPoint.position.y)
        {
            fixHandPosition.x = gameObject.transform.position.x;
            fixHandPosition.y = bottomRightFixHandPoint.position.y;

            // 手のY軸を固定する地点から右に行ったら手のX軸を固定する
            if (gameObject.transform.position.x >= bottomRightFixHandPoint.position.x)
            {
                fixHandPosition.x = bottomRightFixHandPoint.position.x;
            }

            // 手のY軸を固定する地点から左に行ったら手のX軸を固定する
            if (gameObject.transform.position.x <= topLeftFixHandPoint.position.x)
            {
                fixHandPosition.x = topLeftFixHandPoint.position.x;
            }
        }
    }
}