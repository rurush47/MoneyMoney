using UnityEngine;
using System.Collections;

public class Note : Entity {
    private GameObject leftCoin;
    private GameObject rightCoin;
    private bool moving = true;

	// Use this for initialization
	void Start () {
		pos = gameObject.transform.position;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		Addition = sprite.bounds.size.x;
		map = FindObjectOfType<Map>();
        grid = map.getGrid();
		spawner = FindObjectOfType<Spawner>();
        leftCoin = gameObject.transform.Find("coin1").gameObject;
        rightCoin = gameObject.transform.Find("coin2").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A) && moving)
        {
            
            posUpdate();

        }

        if (Input.GetKeyDown(KeyCode.D) && moving)
        {
            posUpdate();
        }
    }

    /*private void moveLeft()
    {
        Vector2 fixedPos = getFixedPosition();
        if (fixedPos.x > 0 && grid[(int)fixedPos.x - 1, (int)fixedPos.y] == null)
        {
            grid[(int)pos.x , (int)pos.y] = null;
            grid[(int)pos.x - 1, (int)pos.y] = this;
            return new Vector2(pos.x - 1, pos.y);
        }
        return pos;
    }*/

    private void moveRight()
    {

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

    public bool isMoving()
    {
        return moving;
    }

    private void posUpdate()
    {
        gameObject.transform.position = pos;
    }
}
