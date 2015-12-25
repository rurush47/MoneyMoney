using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    //TODO dziedziczone variables 

    public Spawner spawner;
    public Map map;
    public Vector2 pos;
    public float Addition;
    public Entity[,] grid;

    public GameObject getGameObject()
    {
        return gameObject;
    }
}
