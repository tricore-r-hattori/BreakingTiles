using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFreezePosition : MonoBehaviour
{
    //[SerializeField]
    //RectTransform hand;

    //[SerializeField]
    //RectTransform tile;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("hit");
    }
}
