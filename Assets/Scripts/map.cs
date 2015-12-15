using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour {



    [SerializeField]
    public int width = 10;
    [SerializeField]
    public int heigth = 20;
    [SerializeField]
    public GameObject Coin;
    public Spawner objSpawner;
    public float gameUpdateSpeed;
    private float time;
    private Entity[,] grid;
    private List<Coin> coins = new List<Coin>();
    private List<Piggy> piggies = new List<Piggy>();

    Map()
    {
        grid = new Entity[width, heigth];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                grid[i, j] = null;
            }
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.S))
        {
            time = 0;
            gameUpdate();
        }

        if(time >= gameUpdateSpeed)
        {
            time = 0;
            gameUpdate();
        }
    }

    void gameUpdate()
    {
        for (int i = width - 1; i >= 0; --i)
        {
            for (int j = heigth - 1; j >= 0; --j)
            {
                if (grid[i,j] != null && grid[i,j] is Coin)
                {
                    grid[i, j].getGameObject().GetComponent<Coin>().moveDown();
                }
            }
        }
    }

    public Vector2 moveLeft(Vector2 pos, Coin entity)
    {
        if ((int)pos.x > 0 && grid[(int)pos.x - 1, (int)pos.y] == null)
        {
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x - 1, (int)pos.y] = entity;
            return new Vector2(pos.x - 1, pos.y);
        }
        return pos;
    }

    public Vector2 moveRight(Vector2 pos, Coin entity)
    {
        if ((int)pos.x < (width - 1) && grid[(int)pos.x + 1, (int)pos.y] == null)
        {
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x + 1, (int)pos.y] = entity;
            return new Vector2(pos.x + 1, pos.y);
        }
        return pos;
    }

    public Vector2 moveDown(Vector2 pos, Coin entity)
    {
        if ((pos.y + 1) == (heigth - 1) || grid[(int)pos.x, (int)pos.y + 2] != null)
        {
            entity.stop();
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x, (int)pos.y + 1] = entity;
            return new Vector2(pos.x, pos.y + 1);
        }
        else
            grid[(int)pos.x, (int)pos.y] = null;
            grid[(int)pos.x, (int)pos.y + 1] = entity;
            return new Vector2(pos.x, pos.y + 1);
    }

    public void mapAppend(Coin newEntity)
    {
        Vector2 pos = newEntity.getFixedPosition();

        coins.Add(newEntity);

        grid[(int)pos.x, (int)pos.y] = newEntity;
    }

    public void mapAppend(Piggy newEntity)
    {
        Vector2 pos = newEntity.getFixedPosition();

        piggies.Add(newEntity);

        grid[(int)pos.x, (int)pos.y - 1] = newEntity;
        grid[(int)pos.x + 1, (int)pos.y - 1] = newEntity;
        grid[(int)pos.x + 1, (int)pos.y] = newEntity;
        grid[(int)pos.x, (int)pos.y] = newEntity;
    }

    public Vector2 randomizeInitPos()
    {
        return new Vector2(Mathf.Floor(Random.Range(0, width - 1)),
        Mathf.Floor(Random.Range(4, heigth)));
    }

    public Entity[,] getGrid()
    {
        return grid;
    }

    public List<Coin> getCoins()
    {
        return coins;
    }

    public List<Piggy> getPiggies()
    {
        return piggies;
    }
    
}
