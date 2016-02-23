using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
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

    public int numberOfPiggies;
	private Map _map;
	private Vector2 _initialvector2 = new Vector2(23,0);

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
			InstantiateCoin();
		}
		if (value >= 1 && value < 2)
		{
			InstantiateNote();
		}
	}

	private float RandomizeSpawningObj()
	{
		return Random.Range(0, 2);
	}

	private float RandomizeType()
	{
		return (Random.Range(0, 3));
	}

    public void InstantiateCoin()
	{
		float value = RandomizeType();
		if (value >= 0 && value < 1)
		{
			GameObject newObj = Instantiate(CoinDollar, _initialvector2, Quaternion.identity) as GameObject;
            Coin newCoin = newObj.GetComponent<Coin>();
            newCoin.Type = MoneyType.Dollar;

            if (newObj != null)
			{
				_map.mapAppend(newCoin);
			}
		}
		if (value >= 1 && value < 2)
		{
			GameObject newObj = Instantiate(CoinPound, _initialvector2, Quaternion.identity) as GameObject;
            Coin newCoin = newObj.GetComponent<Coin>();
            newCoin.Type = MoneyType.Pound;

            if (newObj != null)
			{
				_map.mapAppend(newCoin);
			}
		}
		if (value >= 2 && value < 3)
		{
			GameObject newObj = Instantiate(CoinEuro, _initialvector2, Quaternion.identity) as GameObject;
            Coin newCoin = newObj.GetComponent<Coin>();
            newCoin.Type = MoneyType.Euro;

            if (newObj != null)
			{
				_map.mapAppend(newCoin);
			}
		}

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

	public void InstantiateNote()
	{
		float value = RandomizeType();
		if (value >= 0 && value < 1)
		{
			GameObject newObj = Instantiate(NoteDollar, _initialvector2, Quaternion.identity) as GameObject;
            Note newNote = newObj.GetComponent<Note>();
            newNote.Type = MoneyType.Dollar;

            if (newObj != null)
			{
				_map.mapAppend(newNote);
			}
		}
		if (value >= 1 && value < 2)
		{
			GameObject newObj = Instantiate(NotePound, _initialvector2, Quaternion.identity) as GameObject;
            Note newNote = newObj.GetComponent<Note>();
            newNote.Type = MoneyType.Pound;

            if (newObj != null)
			{
				_map.mapAppend(newNote);
			}
		}
		if (value >= 2 && value < 3)
		{
			GameObject newObj = Instantiate(NoteEuro, _initialvector2, Quaternion.identity) as GameObject;
            Note newNote = newObj.GetComponent<Note>();
            newNote.Type = MoneyType.Euro;

            if (newObj != null)
			{
				_map.mapAppend(newNote);
			}
		}
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
                    && bannedFields[initFixedVec.x, initFixedVec.y - 1] && bannedFields[initFixedVec.x, initFixedVec.y + 1])
                {
                    GameObject newObj = Instantiate(PiggyPound, initvector2, Quaternion.identity) as GameObject;
                    if (newObj != null)
                    {
                        Piggy newPiggy = newObj.GetComponent<Piggy>();
                        newPiggy.Type = MoneyType.Pound;
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
}
