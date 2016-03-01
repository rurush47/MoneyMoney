using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour
{

    public class LeftButton : MonoBehaviour
    {
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
}


