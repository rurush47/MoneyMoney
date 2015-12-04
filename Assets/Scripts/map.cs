using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class map : MonoBehaviour {

    [SerializeField]
    public int width = 10;
    [SerializeField]
    public int heigth = 20;
    [SerializeField]
    public GameObject Coin;
    public spawner objSpawner;
    private coin[,] grid;

    public map()
    {
        grid = new coin[width, heigth];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                grid[i, j] = null;
            }
        }  
    } 

    public Vector2 moveLeft(Vector2 pos, coin entity)
    {
        if ((int)pos.x > 0 && grid[(int)pos.x - 1, (int)pos.y] == null)
        {
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x - 1, (int)pos.y] = entity;
            return new Vector2(pos.x - 1, pos.y);
        }
        return pos;
    }

    public Vector2 moveRight(Vector2 pos, coin entity)
    {
        if ((int)pos.x < width && grid[(int)pos.x + 1, (int)pos.y] == null)
        {
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x + 1, (int)pos.y] = entity;
            return new Vector2(pos.x + 1, pos.y);
        }
        return pos;
    }

    public Vector2 moveDown(Vector2 pos, coin entity)
    {
        if((int)pos.y < heigth && grid[(int)pos.x, (int)pos.y + 1] == null)
        {
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x, (int)pos.y + 1] = entity;
            pos = new Vector2(pos.x, pos.y + 1);
        }
        if ((int)pos.y == heigth - 1 /*|| grid[(int)pos.x, (int)pos.y + 1] != null*/)
        {
            entity.stop();
            objSpawner.instanciate();
        }
        return pos;
    }

    public void mapAppend(coin newEntity)
    {
        Vector2 pos = newEntity.getFixedPosition();
        Debug.Log(pos);
        grid[(int)pos.x, (int)pos.y] = newEntity;
    }


}
