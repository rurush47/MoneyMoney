using UnityEngine;
using System.Collections;

public class Coin : Entity {
	public Spawner spawner;
	private Map grid;
	private Vector2 pos;
	private float Addition;
	private bool moving = true;
	private GameObject gameObj;
	

	void Awake () {
		pos = gameObject.transform.position;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		Addition = sprite.bounds.size.x;
		grid = FindObjectOfType<Map>();
		spawner = FindObjectOfType<Spawner>();
	}
	
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
	}

	public void moveDown()
	{
		if (moving)
		{
			pos = getRealPosition(grid.moveDown(getFixedPosition(), this));
			posUpdate();

			if(!moving)
			{
				spawner.instantiateCoin();
			}
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

	public void coinCheck()
	{
		Vector2 fixedPos = getFixedPosition();
		Entity[,] currentGrid = grid.getGrid();
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
		Entity[,] currentGrid = grid.getGrid();

		for (int i = 0; i < 4; i++)
		{
			GameObject toDestroy = currentGrid[x, y - i].getGameObject();
			grid.getCoins().Remove(toDestroy.GetComponent<Coin>());
			Destroy(toDestroy);
			currentGrid[x, y - i] = null;
		}
	}
}
