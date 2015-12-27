using UnityEngine;
using System.Collections;

public class Note : Entity {
    private GameObject leftCoin;
    private GameObject rightCoin;
    private bool moving = true;

	// Use this for initialization
	void Awake () {
		pos = gameObject.transform.position;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		Addition = sprite.bounds.size.x / 2;
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
            moveLeft();
            posUpdate();

        }

        if (Input.GetKeyDown(KeyCode.D) && moving)
        {
            moveRight();
            posUpdate();
        }
    }

    public void moveLeft()
    {
        Vector2 fixedPos = getFixedPosition();
        //Debug.Log(grid[(int)fixedPos.x - 1, (int)fixedPos.y]);
        
        if ((int)fixedPos.x > 0 && grid[(int)fixedPos.x - 1, (int)fixedPos.y] == null)
        {
            grid[(int)fixedPos.x + 1, (int)fixedPos.y] = null;
            grid[(int)fixedPos.x - 1, (int)fixedPos.y] = this;
            fixedPos = new Vector2(fixedPos.x - 1, fixedPos.y);
        }
        pos = getRealPosition(fixedPos);
    }

    public void moveRight()
    {
        Vector2 fixedPos = getFixedPosition();

        if ((int)fixedPos.x < (map.width - 2) && grid[(int)fixedPos.x + 2, (int)fixedPos.y] == null)
        {
            grid[(int)fixedPos.x, (int)fixedPos.y] = null;
            grid[(int)fixedPos.x + 2, (int)fixedPos.y] = this;
            fixedPos = new Vector2(fixedPos.x + 1, fixedPos.y);
        }
        Debug.Log(fixedPos);
        pos = getRealPosition(fixedPos);
    }


    public void moveDown()
    {
        if (moving)
        {
            Vector2 fixedPos = getFixedPosition();

            if ((fixedPos.y + 1) == (map.heigth - 1) || grid[(int)fixedPos.x, (int)fixedPos.y + 2] != null)
            {
                stop();
                grid[(int)fixedPos.x, (int)fixedPos.y] = null;
                grid[(int)fixedPos.x, (int)fixedPos.y + 1] = this;
                fixedPos = new Vector2(fixedPos.x, fixedPos.y + 1);
            }
            else
            {
                grid[(int)fixedPos.x, (int)fixedPos.y] = null;
                grid[(int)fixedPos.x, (int)fixedPos.y + 1] = this;
                fixedPos = new Vector2(fixedPos.x, fixedPos.y + 1);
            }

            pos = getRealPosition(fixedPos);
            posUpdate();
        }
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
