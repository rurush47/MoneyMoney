﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour {

    //grid init values
    [SerializeField] public int Width;
    [SerializeField] public int Heigth;
    private Entity[,] _grid;
    //game relevant values
    public Spawner ObjSpawner;
    private float GameUpdateSpeed = SceneManager.gameSpeed;
    private float _time;
    //obj vactors
    private List<Coin> _coins = new List<Coin>();
    private List<Piggy> _piggies = new List<Piggy>();
    private List<Note> _notes = new List<Note>();
    private List<Block> _blocks = new List<Block>();

    private Entity _currentObj;

    public bool test = true;


    void Awake()
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
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCurrentObjLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCurrentObjRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }

        if (_time >= GameUpdateSpeed)
        {
            _time = 0;
            GameUpdate();
        }
    }

    void GameUpdate()
    {
        //somehow foreach gives a bug
         for (int i = 0; i < _piggies.Count; i++)
        {
                _piggies[i].CoinCheck();
        }


        for (int i = _coins.Count - 1; i >= 0 ; --i)
        {
            if(!_coins[i].IsMoving())
                _coins[i].CoinCheck();
        }

        for (int i = _coins.Count - 1; i >= 0; --i)
        {
            if(_coins[i].toBeErased)
            {
                EraseCoinsAt(i);
            }
        }
        //items movement

        for(int j = Heigth - 1; j >= 0; --j)
        {
            for(int i = Width - 1; i >= 0; --i)
            {
                if (_grid[i, j] is Coin)
                {
                    Coin coin = _grid[i, j].GetComponent<Coin>();
                    if (coin.IsMoving())
                    {
                        coin.MoveDown();
                    }
                }
            }
        }
        //notes movement only changes their sprite pos
        for (int i = 0; i < _notes.Count; i++)
        {
            if (_notes[i].IsMoving())
                _notes[i].MoveDown();
        }
        //same for block
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].IsMoving())
                _blocks[i].MoveDown();
        }

        //checking for movement

        bool isMovement = false;

        for (int i = 0; i < _coins.Count; i++)
        {
            if (_coins[i].IsMoving())
                isMovement = true;
        }

        GameOverCheck();
        WinCheck();

        if (!isMovement)
            ObjSpawner.InstantiateNewObj();

    }

    public void mapAppend(Coin newEntity)
    {
        IntVector2 pos = newEntity.GetFixedPosition();

        _coins.Add(newEntity);
        _currentObj = newEntity;

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
        _currentObj = newEntity;

        _coins.Add(newEntity.GetLeftCoin());
        _coins.Add(newEntity.GetRightCoin());

        _grid[pos.x, pos.y] = newEntity.GetLeftCoin();
        _grid[pos.x + 1, pos.y] = newEntity.GetRightCoin();

    }

    public void mapAppend(Block newEntity)
    {
        _blocks.Add(newEntity);
        _currentObj = newEntity;
    }

    public Vector2 RandomizeInitPos()
    {
        return new Vector2(Mathf.Floor(Random.Range(0, Width - 1)),
        Mathf.Floor(Random.Range(7, Heigth - 1)));
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

    public List<Block> GetBlocks()
    {
        return _blocks;
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

        MoveCoinsAbove();
    }

    public void EraseCoinsAt(int i)
    {
        IntVector2 fixedPos = _coins[i].GetFixedPosition();
        GameObject toDestroy = _coins[i].GetGameObject();
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
        _coins.RemoveAt(i);
        Destroy(toDestroy);
        _grid[fixedPos.x, fixedPos.y] = null;
     
        MoveCoinsAbove();
    }

    public void MoveCoinsAbove()
    {
        //double loop to make all coins fall properly after score
        {
            Coin currentCoin;
            for (int i = Heigth - 2; i >= 0; --i)
            {
                for (int j = Width - 1; j >= 0; --j)
                {
                    if (_grid[j, i] != null && _grid[j, i] is Coin)
                    {
                        currentCoin = _grid[j, i].GetComponent<Coin>();

                        if(currentCoin.HasNote)
                        {
                            if (currentCoin.GetNote().CanMoveDown())
                            {
                                currentCoin.Move();
                            }
                        }
                        else
                        {
                            if (CanMoveDown(j,i))
                            {
                                currentCoin.Move();
                            }
                        }
                    }
                }
            }
        }
    }

    public bool CanMoveDown(int x, int y)
    {
        if (x >= 0 && y < Heigth - 1 && x < Width - 1 && y >= 0)
        {
            if (_grid[x, y + 1] == null || _grid[x, y + 1] != null
                && _grid[x, y + 1].IsMoving())
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public bool CanMoveRight(int x, int y)
    {
        if (x < Width  && _grid[x + 1, y] == null ||
            _grid[x + 1, y] != null && _grid[x + 1, y].GetComponent<Entity>().IsMoving())
        {
            return true;
        }
        else
            return false;
    }

    public bool CanMoveLeft(int x, int y)
    {

        if (x > 0 && _grid[x - 1, y] == null ||
            _grid[x - 1, y] != null && _grid[x - 1, y].GetComponent<Entity>().IsMoving())
        {
            return true;
        }
        else
            return false;
    }

    public string TypeName(MoneyType type)
    {
        if(type == MoneyType.Dollar)
        {
            return "Dollar";
        }
        else if (type == MoneyType.Euro)
        {
            return "Euro";
        }
        else
        {
            return "Pound";
        }
    }

    public void GameOverCheck()
    {
        for(int j = 0; j < 2; ++j)
        {
            for(int i = 0; i < Width; ++i)
            {
                if (_grid[i,j] != null && !_grid[i,j].GetComponent<Entity>().IsMoving())
                {
                    Application.LoadLevel("Menu");
                }
            }
        }
    }

    public void WinCheck()
    {
        if (_piggies.Count <= 0)
        {
            SceneManager.gameSpeed -= 0.1f;
            SceneManager.numberOfPiggies += 1;
            Application.LoadLevel("Scene1");
        }
    }

    public void MoveEvent()
    {

    }

    public void MoveCurrentObjLeft()
    {
        if(_currentObj is Coin)
        {
            _currentObj.GetComponent<Coin>().MoveLeft();
        }
        else if (_currentObj is Note)
        {
            _currentObj.GetComponent<Note>().MoveLeft();
        }
        else if (_currentObj is Block)
        {
            _currentObj.GetComponent<Block>().MoveLeft();
        }
    }

    public void MoveCurrentObjRight()
    {
        if (_currentObj is Coin)
        {
            _currentObj.GetComponent<Coin>().MoveRight();
        }
        else if (_currentObj is Note)
        {
            _currentObj.GetComponent<Note>().MoveRight();
        }
        else if (_currentObj is Block)
        {
            _currentObj.GetComponent<Block>().MoveRight();
        }
    }

    public void Rotate()
    {
        if (_currentObj is Note)
        {
            _currentObj.GetComponent<Note>().Rotate();
        }
        if (_currentObj is Block)
        {
            _currentObj.GetComponent<Block>().Rotate();
        }
    }

    public void MoveDown()
    {
        _time = 0;
        GameUpdate();
    }
}
