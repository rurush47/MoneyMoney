﻿using UnityEngine;
using System.Collections;

public class Block : Entity {

    public Entity[,] blockGrid;
    private Vector2 _initPos = new Vector2(23, -23);
    private IntVector2 intInitPosGlobal;
    private Map map;

	void Awake ()
    {
        map = FindObjectOfType<Map>();
        intInitPosGlobal = new IntVector2((map.Width/ 2) - 1, 0);
        Map = FindObjectOfType<Map>();
        Grid = Map.GetGrid();
        Spawner = FindObjectOfType<Spawner>();
		Pos = gameObject.transform.position;

        blockGrid = new Entity[2, 2];
        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                blockGrid[i,j] = null;
            }
        }

    }

    public void Append()
    {
        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                if (blockGrid[i, j] == null)
                {
                    float value = RandomizeTypeOfObj();
                    if (value >= 0 && value < 2)
                    {
                        IntVector2 intInitPos = new IntVector2(i + intInitPosGlobal.x, j);
                        Vector2 initPos = GetRealPosition(intInitPos);

                        blockGrid[i, j] = Spawner.InstantiateCoin(initPos).GetComponent<Coin>();
                    }
                    else if (value >= 2 && value <= 3)
                    {
                        //Note
                        //horizontal
                        if (blockGrid[(i + 1) % 2, j] == null)
                        {
                            IntVector2 intInitPos = new IntVector2(0 + intInitPosGlobal.x, j);
                            Vector2 initPos = GetRealPosition(intInitPos);
                            //spawn block in j
                            blockGrid[0, j] = Spawner.InstantiateNote(initPos).GetComponent<Note>().GetLeftCoin();
                            blockGrid[1, j] = blockGrid[0, j].GetComponent<Coin>()._note.GetRightCoin();
                        }
                        //vertical
                        else if (blockGrid[i, (j + 1) % 2] == null)
                        {
                            IntVector2 intInitPos = new IntVector2(i + intInitPosGlobal.x, 1);
                            Vector2 initPos = GetRealPosition(intInitPos);
                            //spawn block in i
                            blockGrid[i, 1] = Spawner.InstantiateNoteVertical(initPos).GetComponent<Note>().GetLeftCoin();
                            blockGrid[1, 0] = blockGrid[i, 1].GetComponent<Coin>()._note.GetRightCoin();
                        }
                    }
                }
            }
        }
    }

    public void MoveLeft()
    {
        bool moved = false;

        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                if(blockGrid[j, i] is Coin)
                {
                    if (blockGrid[j, i].GetComponent<Coin>().HasNote)
                    {
                        if (blockGrid[j, i].GetComponent<Coin>().GetNote().GetLeftCoin() == blockGrid[j, i].GetComponent<Coin>())
                        {
                            blockGrid[j, i].GetComponent<Coin>().GetNote().MoveLeft();
                        }
                    }
                    else
                    {
                        blockGrid[j, i].GetComponent<Coin>().MoveLeft();
                    }
                    moved = true;
                }
                else
                {
                    
                }
            }
        }

        if(moved)
        {
            transform.position -= new Vector3(Addition, 0, 0);
        }
    }

    public void MoveRight()
    {
        bool moved = false;

        for (int i = 1; i >= 0; --i)
        {
            for (int j = 1; j >= 0; --j)
            {
                if (blockGrid[j, i] is Coin)
                {
                    if (blockGrid[j, i].GetComponent<Coin>().HasNote)
                    {
                        if (blockGrid[j, i].GetComponent<Coin>().GetNote().GetLeftCoin() == blockGrid[j, i].GetComponent<Coin>())
                        {
                            blockGrid[j, i].GetComponent<Coin>().GetNote().MoveRight();
                        }
                    }
                    else
                    {
                        blockGrid[j, i].GetComponent<Coin>().MoveRight();
                    }
                    moved = true;
                }
                else
                {

                }
            }
        }

        if(moved)
        {
            transform.position += new Vector3(Addition, 0, 0);
        }
    }

    public void StopCheck()
    {
        bool areMoving = false;

        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                if (blockGrid[i, j] != null && blockGrid[i, j].IsMoving())
                {
                    areMoving = true;
                }
            }
        }

        if (!areMoving)
        {
            transform.position -= new Vector3(0, Addition, 0);
            Stop();
        }
            
    }

    public void Stop()
    {
        _moving = false;
    }

    public void MoveDown()
    {
        StopCheck();
        if(_moving)
        {
            transform.position -= new Vector3(0, Addition, 0);
        }
    }
	
    private float RandomizeTypeOfObj()
    {
        return Random.Range(0, 2);
    }


    public void Rotate()
    {/*
        Entity[,] grid = Map.GetGrid();
        IntVector2 rotation = new IntVector2(1, 0);
        IntVector2 blockPos = GetFixedPosition();

        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                //not best idea but to tired to coin sth else
                IntVector2 clockVector;

                if (i == 0 && j == 0)
                {
                    clockVector = new IntVector2(1, 0);
                }
                else if (i == 1 && j == 0)
                {
                    clockVector = new IntVector2(0, 1);
                }
                else if (i == 0 && j == 1)
                {
                    clockVector = new IntVector2(0, -1);
                }
                else
                {
                    clockVector = new IntVector2(-1, 0);
                }

                //blockGrid[i, j] = blockGrid[i + clockVector.x, j + clockVector.y];
                //grid[blockPos.x, blockPos.y] = blockGrid[(blockPos.x + rotation.x) % 2, (blockPos.y + rotation.y) % 2];

                if (blockGrid[i, j] != null)
                {
                    blockGrid[i, j].gameObject.transform.position +=
                        new Vector3(clockVector.x * Addition, clockVector.y * Addition, 0);
                }

                int temp = rotation.x;
                rotation = new IntVector2(rotation.y, temp);
            }
        }*/
    }
}
