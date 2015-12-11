using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    public GameObject coin;
    public GameObject piggy;
    public GameObject bankNote;
    public GameObject map;
    private map grid;
    private Vector2 initialPos = new Vector2(23,0);
	// Use this for initialization
	void Start () {
        grid = map.GetComponent<map>();
        instantiatePiggy();
        instantiateCoin();
	}
	
	// Update is called once per frame
	void Update () {
	
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
            coin newCoin = newObj.GetComponent<coin>();
            grid.mapAppend(newCoin);
        }

    }

    public void instantiatePiggy()
    { 
        Vector2 initPos = new Vector2(grid.randomizeInitPos().x * 23, -grid.randomizeInitPos().y * 23); 
        GameObject newObj = Instantiate(piggy, initPos, Quaternion.identity) as GameObject;
        piggy newPiggy = newObj.GetComponent<piggy>();
        grid.mapAppend(newPiggy);
    }
}
