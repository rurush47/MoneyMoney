﻿using UnityEngine;
using System.Collections;

public class Block : Entity {

    public Entity[,] blockGrid;
    private Vector2 _initPos = new Vector2(23, -23);

	void Awake ()
    {
        Map = FindObjectOfType<Map>();
        Grid = Map.GetGrid();
        Spawner = FindObjectOfType<Spawner>();

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
                        IntVector2 intInitPos = new IntVector2(i, j);
                        Vector2 initPos = GetRealPosition(intInitPos);

                        blockGrid[i, j] = Spawner.InstantiateCoin(initPos).GetComponent<Coin>();
                    }
                    else if (value >= 2 && value <= 3)
                    {
                        //Note
                        //horizontal
                        if (blockGrid[(i + 1) % 2, j] == null)
                        {
                            IntVector2 intInitPos = new IntVector2(0, j);
                            Vector2 initPos = GetRealPosition(intInitPos);
                            //spawn block in j
                            blockGrid[0, j] = Spawner.InstantiateNote(initPos).GetComponent<Note>().GetLeftCoin();
                            blockGrid[1, j] = blockGrid[0, j].GetComponent<Coin>()._note.GetRightCoin();
                        }
                        //vertical
                        else if (blockGrid[i, (j + 1) % 2] == null)
                        {
                            IntVector2 intInitPos = new IntVector2(i, 1);
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
        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                if(blockGrid[j, i] is Coin)
                {
                    if (blockGrid[j, i].GetComponent<Coin>().HasNote)
                    {
                        if (blockGrid[j, i].GetComponent<Coin>().GetNote().GetLeftCoin() == blockGrid[j, i].GetComponent<Coin>())
                            blockGrid[j, i].GetComponent<Coin>().GetNote().MoveLeft();
                    }
                    else
                        blockGrid[j, i].GetComponent<Coin>().MoveLeft();

                }
                else
                {
                    
                }
            }
        }
    }

    public void MoveRight()
    {
        for (int i = 1; i >= 0; --i)
        {
            for (int j = 1; j >= 0; --j)
            {
                if (blockGrid[j, i] is Coin)
                {
                    if (blockGrid[j, i].GetComponent<Coin>().HasNote)
                    {
                        if (blockGrid[j, i].GetComponent<Coin>().GetNote().GetLeftCoin() == blockGrid[j, i].GetComponent<Coin>())
                            blockGrid[j, i].GetComponent<Coin>().GetNote().MoveRight();
                    }
                    else
                        blockGrid[j, i].GetComponent<Coin>().MoveRight();

                }
                else
                {

                }
            }
        }
    }
	
    private float RandomizeTypeOfObj()
    {
        return Random.Range(0,3);
    }


    public void Rotate()
    {

    }

}
