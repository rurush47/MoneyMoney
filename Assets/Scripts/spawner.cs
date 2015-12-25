using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject coin;
	public GameObject piggy;
	public GameObject note;
	public GameObject map;
	private Map grid;
	private Vector2 initialPos = new Vector2(23,0);


	void Start ()
	{
		grid = map.GetComponent<Map>();
		instantiatePiggy();
		instantiateNote();
	}
	
	private float randomize()
	{
		return(Random.Range(0,1));
	}
	
	public void instantiateCoin()
	{
		float value = randomize();
		if (value >= 0 && value < 1)
		{
			GameObject newObj = Instantiate(coin, initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.GetComponent<Coin>();
			grid.mapAppend(newCoin);
		}

	}

	public void instantiateNote()
	{
		{
			GameObject newObj = Instantiate(note, initialPos, Quaternion.identity) as GameObject;
			Coin newCoin = newObj.transform.Find("coin1").GetComponent<Coin>();
			Coin newCoin2 = newObj.transform.Find("coin2").GetComponent<Coin>(); 
			grid.mapAppend(newCoin);
			grid.mapAppend(newCoin2);
		}
	}

	public void instantiatePiggy()
	{ 
		Vector2 initPos = new Vector2(grid.randomizeInitPos().x * 23, -grid.randomizeInitPos().y * 23); 
		GameObject newObj = Instantiate(piggy, initPos, Quaternion.identity) as GameObject;
		Piggy newPiggy = newObj.GetComponent<Piggy>();
		grid.mapAppend(newPiggy);
	}
}
