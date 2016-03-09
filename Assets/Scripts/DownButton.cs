using UnityEngine;
using System.Collections;

public class DownButton : MonoBehaviour {

    Map map;

    void Awake()
    {
        map = FindObjectOfType<Map>();
    }

    void OnMouseDown()
    {
        map.MoveDown();
    }
}
