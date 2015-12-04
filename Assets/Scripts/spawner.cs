using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    public GameObject coin;
    public GameObject bankNote;
    public GameObject map;
    private map grid;
    private Vector2 initialPos = new Vector2(23,0);
	// Use this for initialization
	void Start () {
        grid = map.GetComponent<map>();
        instanciate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private float randomize()
    {
        return(Random.Range(0,1));
    }

    public void instanciate()
    {
        float value = randomize();
        if (value >= 0 && value < 1)
        {
            GameObject newObj = Instantiate(coin, initialPos, Quaternion.identity) as GameObject;
            coin newCoin = newObj.GetComponent<coin>();
            Debug.Log(newObj.transform.position);
            grid.mapAppend(newCoin);
        }

    }

}
