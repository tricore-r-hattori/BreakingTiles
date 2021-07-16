using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SwipeForceCalculation : MonoBehaviour
{
    [SerializeField]
    GameObject tile;

    Vector3 startPos; //タップした場所を記録
    Vector3 endPos;　//指を離した場所を記録
    float dir = 0.0f;　//スワイプした距離を取得
    float speed = 0.0f;
    float timeCount = 0.0f;
    const float time = 30.0f;

    const float shiftingPositionsY = 80.0f;

    // Update is called once per frame
    void Update()
    {
        timeCount++;

        if (timeCount % 30.0f == 0)
        {
            startPos = Input.mousePosition;
            timeCount = 0.0f;
        }

        //指を離した場所を取得する
        if (Input.GetMouseButtonUp(0))
        {
            endPos = tile.transform.position;
            endPos.y += shiftingPositionsY;

            //スワイプした距離を取得する
            dir = Mathf.Abs(Vector3.Distance(startPos, endPos));

            //速度を計算する
            speed = dir / time;

            Debug.Log(speed);
        }
    }
}