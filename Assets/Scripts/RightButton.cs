using UnityEngine;
using System.Collections;

public class RightButton : MonoBehaviour {

    Map map;

    void Awake()
    {
        map = FindObjectOfType<Map>();
    }

    void OnMouseDown()
    {
        map.MoveCurrentObjRight();
    }
}
