using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject coin;
	public GameObject piggy;
	public GameObject note;
	public GameObject gameMap;
	private Map map;
	private Vector2 initialPos = new Vector2(23,0);


	void Start ()
	{
		map = gameMap.GetComponent<Map>();
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
			map.mapAppend(newCoin);
		}

	}

	public void instantiateNote()
	{
		GameObject newObj = Instantiate(note, initialPos, Quaternion.identity) as GameObject;
        Note newNote = newObj.GetComponent<Note>();
        Debug.Log(newNote.getFixedPosition());
        map.mapAppend(newNote);	
	}

	public void instantiatePiggy()
	{ 
		Vector2 initPos = new Vector2(map.randomizeInitPos().x * 23, -map.randomizeInitPos().y * 23); 
		GameObject newObj = Instantiate(piggy, initPos, Quaternion.identity) as GameObject;
		Piggy newPiggy = newObj.GetComponent<Piggy>();
		map.mapAppend(newPiggy);
	}
}
