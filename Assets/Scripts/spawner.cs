using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject coinDollar;
	public GameObject coinPound;
	public GameObject coinEuro;
	public GameObject piggyDollar;
	public GameObject piggyPound;
	public GameObject piggyEuro;
	public GameObject noteDollar;
	public GameObject notePound;
	public GameObject noteEuro;
	public GameObject gameMap;
	private Map map;
	private Vector2 initialPos = new Vector2(23,0);


	void Start ()
	{
		map = gameMap.GetComponent<Map>();
		instantiatePiggy();
		instantiateCoin();
	}
	
	private float randomize()
	{
		return(Random.Range(0, 1));
	}

	private float randomizeType()
	{
		return (Random.Range(0, 3));
	}
	
	public void instantiateCoin()
	{
		float value = randomizeType();
		if (value >= 0 && value < 1)
		{
			GameObject newObj = Instantiate(coinDollar, initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.GetComponent<Coin>();
			newCoin.setType(MoneyType.Dollar);
			map.mapAppend(newCoin);
		}
		if (value >= 1 && value < 2)
		{
			GameObject newObj = Instantiate(coinPound, initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.GetComponent<Coin>();
			newCoin.setType(MoneyType.Pound);
			map.mapAppend(newCoin);
		}
		if (value >= 2 && value < 3)
		{
			GameObject newObj = Instantiate(coinEuro, initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.GetComponent<Coin>();
			newCoin.setType(MoneyType.Euro);
			map.mapAppend(newCoin);
		}

	}

	public void instantiateNote()
	{
		GameObject newObj = Instantiate(noteDollar, initialPos, Quaternion.identity) as GameObject;
		Note newNote = newObj.GetComponent<Note>();
		
		map.mapAppend(newNote);	
	}

	public void instantiatePiggy()
	{ 
		Vector2 initPos = new Vector2(map.randomizeInitPos().x * 23, -map.randomizeInitPos().y * 23); 
		GameObject newObj = Instantiate(piggyDollar, initPos, Quaternion.identity) as GameObject;
		Piggy newPiggy = newObj.GetComponent<Piggy>();
		map.mapAppend(newPiggy);
	}
}
