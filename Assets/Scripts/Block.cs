using UnityEngine;
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
                    if (value >= 0 && value < 1)
                    {
                        IntVector2 intInitPos = new IntVector2(i, j);
                        Vector2 initPos = GetRealPosition(intInitPos);

                        blockGrid[i, j] = Spawner.InstantiateCoin(initPos).GetComponent<Coin>();
                    }

                    else if (value >= 1 && value <= 2)
                    {

                    }
                }
            }
        }
    }

    public void MoveLeft()
    {
        for (int i = 1; i >= 0; --i)
        {
            for (int j = 0; j < 2; ++j)
            {
                if(blockGrid[j,i] is Coin)
                {
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
        return Random.Range(0,1);
    }



	// Update is called once per frame
	void Update () {
	
	}
}
