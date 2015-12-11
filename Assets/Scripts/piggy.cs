using UnityEngine;
using System.Collections;

public class piggy : entity {

    public spawner spawner;
    private map grid;
    private Vector2 pos;
    private float time = 0;
    private float Addition;
    // Use this for initialization
    void Awake()
    {
        pos = gameObject.transform.position;
        Debug.Log("piggy realpos:" + pos);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x / 2;
        grid = FindObjectOfType<map>();
        spawner = FindObjectOfType<spawner>();
    }

    // Update is called once per frame
    void Update () {
        	
	}

    public Vector2 getFixedPosition()
    {
        if (pos.x == 0 && pos.y == 0)
        {
            return new Vector2(0, 0);
        }
        if (pos.x == 0)
        {
            return new Vector2(0, (-pos.y / Addition));
        }
        if (pos.y == 0)
        {
            return new Vector2((pos.x / Addition), 0);
        }
        return new Vector2((pos.x / Addition), (-pos.y / Addition));
    }
}
