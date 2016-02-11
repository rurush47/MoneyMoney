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
	private Map _map;
	private Vector2 _initialPos = new Vector2(23,0);


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
			GameObject newObj = Instantiate(CoinDollar, _initialPos, Quaternion.identity) as GameObject;
            Coin newCoin = newObj.GetComponent<Coin>();
            newCoin.Type = MoneyType.Dollar;

            if (newObj != null)
			{
				_map.mapAppend(newCoin);
			}
		}
		if (value >= 1 && value < 2)
		{
			GameObject newObj = Instantiate(CoinPound, _initialPos, Quaternion.identity) as GameObject;
            Coin newCoin = newObj.GetComponent<Coin>();
            newCoin.Type = MoneyType.Pound;

            if (newObj != null)
			{
				_map.mapAppend(newCoin);
			}
		}
		if (value >= 2 && value < 3)
		{
			GameObject newObj = Instantiate(CoinEuro, _initialPos, Quaternion.identity) as GameObject;
            Coin newCoin = newObj.GetComponent<Coin>();
            newCoin.Type = MoneyType.Euro;

            if (newObj != null)
			{
				_map.mapAppend(newCoin);
			}
		}

	}

	public void InstantiateNote()
	{
		float value = RandomizeType();
		if (value >= 0 && value < 1)
		{
			GameObject newObj = Instantiate(NoteDollar, _initialPos, Quaternion.identity) as GameObject;
            Note newNote = newObj.GetComponent<Note>();
            newNote.Type = MoneyType.Dollar;

            if (newObj != null)
			{
				_map.mapAppend(newNote);
			}
		}
		if (value >= 1 && value < 2)
		{
			GameObject newObj = Instantiate(NotePound, _initialPos, Quaternion.identity) as GameObject;
            Note newNote = newObj.GetComponent<Note>();
            newNote.Type = MoneyType.Pound;

            if (newObj != null)
			{
				_map.mapAppend(newNote);
			}
		}
		if (value >= 2 && value < 3)
		{
			GameObject newObj = Instantiate(NoteEuro, _initialPos, Quaternion.identity) as GameObject;
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
        Vector2 initPos;
        if (!_map.test)
        {
		    initPos = new Vector2(_map.RandomizeInitPos().x * 23, -_map.RandomizeInitPos().y * 23); 
        }
        else
        {
            initPos = new Vector2(23*2, -23*16);
        }
        GameObject newObj = Instantiate(PiggyPound, initPos, Quaternion.identity) as GameObject;
		if (newObj != null)
		{
			Piggy newPiggy = newObj.GetComponent<Piggy>();
			newPiggy.Type = MoneyType.Pound;
			_map.mapAppend(newPiggy);
		}
	}
}
