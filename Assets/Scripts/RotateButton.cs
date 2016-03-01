using UnityEngine;
using System.Collections;

public class RotateButton : MonoBehaviour {

    Map map;

    void Awake()
    {
        map = FindObjectOfType<Map>();
    }

    void OnMouseDown()
    {
        map.Rotate();
    }
}
