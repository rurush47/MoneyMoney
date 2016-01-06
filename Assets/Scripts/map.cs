using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour {

    //grid init values
    [SerializeField] public int width = 10;
    [SerializeField] public int heigth = 20;
    private Entity[,] grid;
    //game relevant values
    public Spawner objSpawner;
    public float gameUpdateSpeed;
    private float time;
    //obj vactors
    private List<Coin> coins = new List<Coin>();
    private List<Piggy> piggies = new List<Piggy>();
    private List<Note> notes = new List<Note>();


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

        if(Input.GetKey(KeyCode.S))
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
        //somehow foreach gives a bug

        for (int i = 0; i < coins.Count; i++)
        {
            if(!coins[i].isMoving())
                coins[i].coinCheck();
        }

        foreach (Piggy element in piggies)
        {
            element.coinCheck();
        }

        //items movement

        for (int i = 0; i < coins.Count; i++)
        {
            if (coins[i].isMoving())
                coins[i].moveDown();
        }

        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].isMoving())
                notes[i].moveDown();
        }
        //checking for movement

        bool isMovement = false;

        for (int i = 0; i < coins.Count; i++)
        {
            if (coins[i].isMoving())
                isMovement = true;
        }

        if (!isMovement)
            objSpawner.instantiateCoin();

    }

    public void mapAppend(Coin newEntity)
    {
        IntVector2 pos = newEntity.getFixedPosition();

        coins.Add(newEntity);

        grid[pos.x, pos.y] = newEntity;
    }

    public void mapAppend(Piggy newEntity)
    {
        IntVector2 pos = newEntity.getFixedPosition();

        piggies.Add(newEntity);

        grid[pos.x, pos.y - 1] = newEntity;
        grid[pos.x + 1, pos.y - 1] = newEntity;
        grid[pos.x + 1, pos.y] = newEntity;
        grid[pos.x, pos.y] = newEntity;
    }

    public void mapAppend(Note newEntity)
    {
        IntVector2 pos = newEntity.getFixedPosition();

        notes.Add(newEntity);

        coins.Add(newEntity.getRightCoin());
        coins.Add(newEntity.getLeftCoin());

        grid[pos.x, pos.y] = newEntity.getLeftCoin();
        grid[pos.x + 1, pos.y] = newEntity.getRightCoin();

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

    public List<Note> getNotes()
    {
        return notes;
    }
    
}
