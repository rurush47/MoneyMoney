﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float objRangeMin;
    public float objRangeMax;

	public GameObject CoinDollar;
	public GameObject CoinPound;
	public GameObject CoinEuro;
	public GameObject PiggyDollar;
	public GameObject PiggyPound;
	public GameObject PiggyEuro;
	public GameObject NoteDollar;
	public GameObject NotePound;
	public GameObject NoteEuro;
	public GameObject GameMap;
    public GameObject Block;

    private int numberOfPiggies = SceneManager.numberOfPiggies;
	private Map _map;
    public Vector2 _initialvector2;

    void Awake()
    {
        _map = FindObjectOfType<Map>();
        _initialvector2 = new Vector2(_map.Width / 2 * 23 - 23, 0);
    }

    void Start ()
	{
		_map = GameMap.GetComponent<Map>();
		InstantiatePiggy();
        InstantiateNewObj();
	}

	public void InstantiateNewObj()
	{
		float value = RandomizeSpawningObj();

		if (value >= 0 && value < 1)
		{
			InstantiateCoin(_initialvector2);
		}
		if (value >= 1 && value < 2)
		{
			InstantiateNote(_initialvector2);
		}
        if (value >= 2 && value < 3)
        {
            InstantiateNoteVertical(_initialvector2 + new Vector2(0, -23));
        }
        if(value >= 3 && value < 4)
        {
            InstantiateBlock(_initialvector2 - new Vector2(23, 0));
        }
    }

	private float RandomizeSpawningObj()
	{
		return Random.Range(objRangeMin, objRangeMax);
	}

	private float RandomizeType()
	{
		return (Random.Range(0, 3));
	}

    private GameObject RandomizePiggy()
    {
        float value = RandomizeType();
        if (value >= 0 && value < 1)
        {
            return PiggyPound;
        }
        else if (value >= 1 && value < 2)
        {
            return PiggyDollar;
        }
        else 
        {
            return PiggyEuro;
        }
    }

    public GameObject RandomizeCoin()
    {
        float value = RandomizeType();
        if (value >= 0 && value < 1)
        {
            return CoinDollar;
        }
        else if (value >= 1 && value < 2)
        {
            return CoinEuro;
        }
        else
        {
            return CoinPound;
        } 
    }

    public GameObject RandomizeNote()
    {
        float value = RandomizeType();
        if (value >= 0 && value < 1)
        {
            return NoteDollar;
        }
        else if (value >= 1 && value < 2)
        {
            return NoteEuro;
        }
        else
        {
            return NotePound;
        }
    }
    public GameObject InstantiateCoin(Vector2 initPos)
	{
		GameObject newObj = Instantiate(RandomizeCoin(), initPos, Quaternion.identity) as GameObject;
        Coin newCoin = newObj.GetComponent<Coin>();

        if (newObj != null)
		{
			_map.mapAppend(newCoin);
		}

        return newObj;
	}

    public IntVector2 ConvertToIntVector(Vector2 vector2)
    {
        if (vector2.x == 0f && vector2.y == 0f)
        {
            return new IntVector2(0, 0);
        }

        if (vector2.x == 0f)
        {
            return new IntVector2(0, (int)(-vector2.y / 23));
        }

        if (vector2.y == 0f)
        {
            return new IntVector2((int)(vector2.x / 23), 0);
        }

        return new IntVector2((int)(vector2.x / 23), (int)(-vector2.y / 23));
    }

	public GameObject InstantiateNote(Vector2 initPos)
	{
			GameObject newObj = Instantiate(RandomizeNote(), initPos, Quaternion.identity) as GameObject;
            Note newNote = newObj.GetComponent<Note>();

            if (newObj != null)
			{
				_map.mapAppend(newNote);
			}

            return newObj;
    }

    public GameObject InstantiateNoteVertical(Vector2 initPos)
    {
        GameObject newObj = Instantiate(RandomizeNote(), initPos, Quaternion.identity) as GameObject;
        Note newNote = newObj.GetComponent<Note>();

        if (newObj != null)
        {
            _map.mapAppend(newNote);
        }

        newNote.Rotate();

        return newObj;
    }

    public void InstantiatePiggy()
	{

        Vector2 initvector2 = new Vector2(_map.RandomizeInitPos().x * 23, -_map.RandomizeInitPos().y * 23);
        IntVector2 initFixedVec = ConvertToIntVector(initvector2);

        bool[,] bannedFields;
        bannedFields = new bool[_map.Width, _map.Heigth];
        for (int i = 0; i < _map.Width; i++)
        {
            for (int j = 0; j < _map.Heigth; j++)
            {
                bannedFields[i, j] = true;
            }
        }

        for (int i = 0; i < numberOfPiggies; ++i)
        {
            bool instantantiated = false;
            while (!instantantiated)
            {

                if (bannedFields[initFixedVec.x,initFixedVec.y] && bannedFields[initFixedVec.x + 1, initFixedVec.y]
                    && bannedFields[initFixedVec.x, initFixedVec.y - 1] && bannedFields[initFixedVec.x + 1, initFixedVec.y - 1])
                {
                    GameObject newObj = Instantiate(RandomizePiggy(), initvector2, Quaternion.identity) as GameObject;
                    if (newObj != null)
                    {
                        Piggy newPiggy = newObj.GetComponent<Piggy>();
                        _map.mapAppend(newPiggy);
                        instantantiated = true;

                        bannedFields[initFixedVec.x, initFixedVec.y] = false;
                        bannedFields[initFixedVec.x + 1, initFixedVec.y] = false;
                        bannedFields[initFixedVec.x, initFixedVec.y - 1] = false;
                        bannedFields[initFixedVec.x + 1, initFixedVec.y - 1] = false;
                    }
                }
                else
                {
                    initvector2 = new Vector2(_map.RandomizeInitPos().x * 23, -_map.RandomizeInitPos().y * 23);
                    initFixedVec = ConvertToIntVector(initvector2);
                }
            }
            instantantiated = false;
        }

        
	}

    public void InstantiateBlock(Vector2 initPos)
    {
        GameObject newObj = Instantiate(Block, initPos, Quaternion.identity) as GameObject;
        newObj.GetComponent<Block>().Append();
        _map.mapAppend(newObj.GetComponent<Block>());
    }
}
