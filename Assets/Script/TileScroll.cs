using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    RectTransform tile;

    Vector3 velocity = Vector3.zero;
    Vector3 MovePoint = Vector3.zero;

    bool IsHit = false;

    const float scrollStartPoint = -1390;
    const float scrollEndPoint = 500;

    void Start()
    {
        velocity = new Vector3(0, Time.deltaTime * speed);
        MovePoint = new Vector3(0.0f, scrollStartPoint, 0.0f);
    }

    void Update()
    {
        if (IsHit)
        {
            //上方向にスクロール
            tile.localPosition += velocity;

            if (tile.localPosition.y >= scrollEndPoint)
            {
                tile.localPosition = MovePoint;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IsHit = true;
    }
}