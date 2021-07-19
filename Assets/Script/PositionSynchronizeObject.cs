using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの座標をマウスの座標と同期するためのクラス
/// </summary>
public class PositionSynchronizeObject : MonoBehaviour
{
	// マウスのスクリーン座標
	Vector3 mouseScreenPosition = new Vector3(0.0f, 0.0f, 0.0f);
	// スクリーン座標をワールド座標に変換したマウスの座標
	Vector3 screenToWorldMousePosition = new Vector3(0.0f, 0.0f, 0.0f);
	// マウスを左クリックした際にプレイヤーと当たったかどうかのフラグ
	bool playerToHit = false;

	/// <summary>
	/// 更新処理
	/// </summary>
	void Update()
	{
		// マウスの左ボタンで押した時
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			playerToHit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
		}

		// 対象のオブジェクトと当たったか
		if (playerToHit)
		{
			// マウスの左ボタンで離した時
			if (Input.GetMouseButton(0))
			{
				// マウスの座標を取得する
				mouseScreenPosition = Input.mousePosition;
				// Z軸修正
				mouseScreenPosition.z = 10f;
				// マウスの座標をスクリーン座標からワールド座標に変換する
				screenToWorldMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
				// ワールド座標に変換されたマウスの座標をアタッチされているオブジェクトの座標に代入
				gameObject.transform.position = screenToWorldMousePosition;
			}
		}
	}
}