using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HandScroll : MonoBehaviour
{
    //スクロールスピード
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    RectTransform hand;

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
            hand.localPosition += velocity;

            if (hand.localPosition.y >= scrollEndPoint)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IsHit = true;
    }
}