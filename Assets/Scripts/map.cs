using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour {

    //grid init values
    [SerializeField] public int Width = 10;
    [SerializeField] public int Heigth = 20;
    private Entity[,] _grid;
    //game relevant values
    public Spawner ObjSpawner;
    public float GameUpdateSpeed;
    private float _time;
    //obj vactors
    private List<Coin> _coins = new List<Coin>();
    private List<Piggy> _piggies = new List<Piggy>();
    private List<Note> _notes = new List<Note>();


    Map()
    {
        _grid = new Entity[Width, Heigth];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                _grid[i, j] = null;
            }
        }
    }

    void Update()
    {
        _time += Time.deltaTime;

        if(Input.GetKey(KeyCode.S))
        {
            _time = 0;
            GameUpdate();
        }

        if(_time >= GameUpdateSpeed)
        {
            _time = 0;
            GameUpdate();
        }
    }

    void GameUpdate()
    {
        //somehow foreach gives a bug
        foreach (Piggy element in _piggies)
        {
            element.CoinCheck();
        }


        for (int i = 0; i < _coins.Count; i++)
        {
            if(!_coins[i].IsMoving())
                _coins[i].CoinCheck();
        }
        //items movement

        for (int i = 0; i < _coins.Count; i++)
        {
            if (_coins[i].IsMoving())
                _coins[i].MoveDown();
        }

        for (int i = 0; i < _notes.Count; i++)
        {
            if (_notes[i].IsMoving())
                _notes[i].MoveDown();
        }
        //checking for movement

        bool isMovement = false;

        for (int i = 0; i < _coins.Count; i++)
        {
            if (_coins[i].IsMoving())
                isMovement = true;
        }

        if (!isMovement)
            ObjSpawner.InstantiateNewObj();

    }

    public void mapAppend(Coin newEntity)
    {
        IntVector2 pos = newEntity.GetFixedPosition();

        _coins.Add(newEntity);

        _grid[pos.x, pos.y] = newEntity;
    }

    public void mapAppend(Piggy newEntity)
    {
        IntVector2 pos = newEntity.GetFixedPosition();

        _piggies.Add(newEntity);

        _grid[pos.x, pos.y - 1] = newEntity;
        _grid[pos.x + 1, pos.y - 1] = newEntity;
        _grid[pos.x + 1, pos.y] = newEntity;
        _grid[pos.x, pos.y] = newEntity;
    }

    public void mapAppend(Note newEntity)
    {
        IntVector2 pos = newEntity.GetFixedPosition();

        _notes.Add(newEntity);

        _coins.Add(newEntity.GetRightCoin());
        _coins.Add(newEntity.GetLeftCoin());

        _grid[pos.x, pos.y] = newEntity.GetLeftCoin();
        _grid[pos.x + 1, pos.y] = newEntity.GetRightCoin();

    }

    public Vector2 RandomizeInitPos()
    {
        return new Vector2(Mathf.Floor(Random.Range(0, Width - 1)),
        Mathf.Floor(Random.Range(4, Heigth)));
    }

    public Entity[,] GetGrid()
    {
        return _grid;
    }

    public List<Coin> GetCoins()
    {
        return _coins;
    }

    public List<Piggy> GetPiggies()
    {
        return _piggies;
    }

    public List<Note> GetNotes()
    {
        return _notes;
    }

    public void EraseCoinsAbove(int x, int y)
    {
        

        for (int i = 0; i < 4; i++)
        {
            GameObject toDestroy = _grid[x, y - i].GetGameObject();
            Coin toDestroyCoin = toDestroy.GetComponent<Coin>();
            //if there is note
            if (toDestroyCoin.HasNote)
            {
                if (toDestroyCoin._note.GetLeftCoin() == toDestroyCoin)
                {
                    toDestroyCoin._note.GetRightCoin().Move();
                    toDestroyCoin._note.GetRightCoin().HasNote = false;
                    toDestroyCoin._note.GetRightCoin()._note = null;
                    toDestroyCoin.HasNote = false;

                    _notes.Remove(toDestroyCoin._note);
                    Destroy(toDestroyCoin._note.gameObject);
                    toDestroyCoin._note = null;
                    //right coin movement
                }
                else
                {
                    toDestroyCoin._note.GetLeftCoin().Move();
                    toDestroyCoin._note.GetLeftCoin().HasNote = false;
                    toDestroyCoin._note.GetLeftCoin()._note = null;
                    toDestroyCoin.HasNote = false;

                    _notes.Remove(toDestroyCoin._note);
                    Destroy(toDestroyCoin._note.gameObject);
                    toDestroyCoin._note = null;
                }
            }
            ////
            _coins.Remove(toDestroyCoin);
            Destroy(toDestroy);
            _grid[x, y - i] = null;
        }
    }

}
