using UnityEngine;
using System.Collections;

public class Coin : Entity {
    public Spawner spawner;
    private Map grid;
    private Vector2 pos;
    private float time = 0;
    private float Addition;
    private bool moving = true;
    private GameObject gameObj;
    // Use this for initialization
    void Awake () {
        pos = gameObject.transform.position;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x;
        grid = FindObjectOfType<Map>();
        spawner = FindObjectOfType<Spawner>();
    }
	
	// Update is called once per frame
	void Update ()
    { 
        if (Input.GetKeyDown(KeyCode.A) && moving)
        {
            pos = getRealPosition(grid.moveLeft(getFixedPosition(), this));
            posUpdate();

        }
        if (Input.GetKeyDown(KeyCode.D) && moving)
        {
            pos = getRealPosition(grid.moveRight(getFixedPosition(), this));
            posUpdate();
        }
        time += Time.deltaTime;
        if (moving && (time >= 1 || Input.GetKey(KeyCode.S)))
        {
            pos = getRealPosition(grid.moveDown(getFixedPosition(), this));
            posUpdate();
            Debug.Log("coin pos:" + pos + "grid:" + getFixedPosition());
            if (!moving)
            {
                spawner.instantiateCoin();
            }
            time = 0;
        }
    }

    private void posUpdate()
    {
        gameObject.transform.position = pos;
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
    
    public Vector2 getRealPosition(Vector2 pos)
    {
        return new Vector2(pos.x, -pos.y) * Addition;
    }

    public void stop()
    {
        moving = false;
    }
}
