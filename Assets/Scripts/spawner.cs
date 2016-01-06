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
		InstantiateCoin();
	}
	
	private float Randomize()
	{
		return(Random.Range(0, 1));
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
			newCoin.SetType(MoneyType.Dollar);
			_map.mapAppend(newCoin);
		}
		if (value >= 1 && value < 2)
		{
			GameObject newObj = Instantiate(CoinPound, _initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.GetComponent<Coin>();
			newCoin.SetType(MoneyType.Pound);
			_map.mapAppend(newCoin);
		}
		if (value >= 2 && value < 3)
		{
			GameObject newObj = Instantiate(CoinEuro, _initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.GetComponent<Coin>();
			newCoin.SetType(MoneyType.Euro);
			_map.mapAppend(newCoin);
		}

	}

	public void InstantiateNote()
	{
		GameObject newObj = Instantiate(NoteDollar, _initialPos, Quaternion.identity) as GameObject;
		Note newNote = newObj.GetComponent<Note>();
		
		_map.mapAppend(newNote);	
	}

	public void InstantiatePiggy()
	{ 
		Vector2 initPos = new Vector2(_map.RandomizeInitPos().x * 23, -_map.RandomizeInitPos().y * 23); 
		GameObject newObj = Instantiate(PiggyDollar, initPos, Quaternion.identity) as GameObject;
		Piggy newPiggy = newObj.GetComponent<Piggy>();
		_map.mapAppend(newPiggy);
	}
}
