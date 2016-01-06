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

    public IntVector2 getFixedPosition()
    {
        if (pos.x == 0f && pos.y == 0f)
        {
            return new IntVector2(0, 0);
        }

        if (pos.x == 0f)
        {
            return new IntVector2(0, (int)(-pos.y / Addition));
        }

        if (pos.y == 0f)
        {
            return new IntVector2((int)(pos.x / Addition), 0);
        }

        return new IntVector2((int)(pos.x / Addition), (int)(-pos.y / Addition));
    }

    public Vector2 getRealPosition(IntVector2 pos)
    {
        return new Vector2(pos.x, -pos.y) * Addition;
    }
}
