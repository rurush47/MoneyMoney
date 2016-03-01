using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    //TODO dziedziczone variables 

    public Spawner Spawner;
    public Map Map;
    public Vector2 Pos;
    public float Addition;
    public Entity[,] Grid;
    public MoneyType Type;
    public bool _moving = true;
    public bool falling = false;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetType(MoneyType newType)
    {
        this.Type = newType;
    }

    public IntVector2 GetFixedPosition()
    {
        if (Pos.x == 0f && Pos.y == 0f)
        {
            return new IntVector2(0, 0);
        }

        if (Pos.x == 0f)
        {
            return new IntVector2(0, (int)(-Pos.y / Addition));
        }

        if (Pos.y == 0f)
        {
            return new IntVector2((int)(Pos.x / Addition), 0);
        }

        return new IntVector2((int)(Pos.x / Addition), (int)(-Pos.y / Addition));
    }

    public Vector2 GetRealPosition(IntVector2 pos)
    {
        return new Vector2(pos.x, -pos.y) * Addition;
    }

    public bool IsMoving()
    {
        return _moving;
    }

    public void Move()
    {
        _moving = true;
    }

    public void MoveLeft()
    {

    }

    public void MoveRight()
    {

    }
}
