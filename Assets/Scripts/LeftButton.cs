﻿using UnityEngine;
using System.Collections;

public class LeftButton : MonoBehaviour
{
    Map map;

    void Awake()
    {
        map = FindObjectOfType<Map>();
    }

    void OnMouseDown()
    {
        map.MoveCurrentObjLeft();
    }
}

