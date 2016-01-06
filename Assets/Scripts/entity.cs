using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    //TODO dziedziczone variables 

    public Spawner spawner;
    public Map map;
    public Vector2 pos;
    public float Addition;
    public Entity[,] grid;
    private MoneyType type;

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void setType(MoneyType newType)
    {
        this.type = newType;
    }
}
