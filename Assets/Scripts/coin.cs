using UnityEngine;
using System.Collections;

public class Coin : Entity {

    private Note note;
    private bool moving = true;
	public bool hasNote = false;


	void Awake () {
		pos = gameObject.transform.position;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		Addition = sprite.bounds.size.x;
		map = FindObjectOfType<Map>();
        grid = map.getGrid();
		spawner = FindObjectOfType<Spawner>();
	}
	
	void Update ()
	{ 
		if (Input.GetKeyDown(KeyCode.A) && moving && !hasNote)
		{
            moveLeft();
		}

		if (Input.GetKeyDown(KeyCode.D) && moving && !hasNote)
		{
			moveRight();
        }
	}

    public void moveLeft()
    {
        if(moving)
        {
            Vector2 fixedPos = getFixedPosition();

            if ((int)fixedPos.x > 0 && grid[(int)fixedPos.x - 1, (int)fixedPos.y] == null)
            {
                grid[(int)fixedPos.x, (int)fixedPos.y] = null;
                grid[(int)fixedPos.x - 1, (int)fixedPos.y] = this;
                fixedPos = new Vector2(fixedPos.x - 1, fixedPos.y);
            }
            pos = getRealPosition(fixedPos);
            posUpdate();
        }
    }

    public void moveRight()
    {
        if (moving)
        {

            Vector2 fixedPos = getFixedPosition();

            if ((int)fixedPos.x < (map.width - 1) && grid[(int)fixedPos.x + 1, (int)fixedPos.y] == null)
            {
                grid[(int)fixedPos.x, (int)fixedPos.y] = null;
                grid[(int)fixedPos.x + 1, (int)fixedPos.y] = this;
                fixedPos = new Vector2(fixedPos.x + 1, fixedPos.y);
            }

            pos = getRealPosition(fixedPos);
            posUpdate();
        }
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

	public bool isMoving()
	{
		return moving;
	}

	public void coinCheck()
	{
		Vector2 fixedPos = getFixedPosition();
		Entity[,] currentGrid = map.getGrid();
		int counter = 0;

		for (int i = 0; i < 3; i++)
		{
			if (((int)fixedPos.y - (i + 1)) >= 0 && currentGrid[(int)fixedPos.x, (int)fixedPos.y - (i + 1)] is Coin)
			{
				++counter;
			}
		}

		if (counter == 3)
		{
			eraseCoinsAbove((int)fixedPos.x, (int)fixedPos.y);
		}
	}

	private void eraseCoinsAbove(int x, int y)
	{
		Entity[,] currentGrid = map.getGrid();

		for (int i = 0; i < 4; i++)
		{
			GameObject toDestroy = currentGrid[x, y - i].getGameObject();
			map.getCoins().Remove(toDestroy.GetComponent<Coin>());
			Destroy(toDestroy);
			currentGrid[x, y - i] = null;
		}
	}

    public void setNote(Note parentNote)
    {
        note = parentNote;
    }
}
