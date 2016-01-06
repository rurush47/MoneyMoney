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
			IntVector2 fixedPos = getFixedPosition();

			if (fixedPos.x > 0 && grid[fixedPos.x - 1, fixedPos.y] == null)
			{
				grid[fixedPos.x, fixedPos.y] = null;
				grid[fixedPos.x - 1, fixedPos.y] = this;
				fixedPos = new IntVector2(fixedPos.x - 1, fixedPos.y);
			}

			pos = getRealPosition(fixedPos);
			posUpdate();
		}
	}

	public void moveRight()
	{
		if (moving)
		{

			IntVector2 fixedPos = getFixedPosition();

			if (fixedPos.x < (map.width - 1) && grid[fixedPos.x + 1, fixedPos.y] == null)
			{
				grid[fixedPos.x, fixedPos.y] = null;
				grid[fixedPos.x + 1, fixedPos.y] = this;
				fixedPos = new IntVector2(fixedPos.x + 1, fixedPos.y);
			}

			pos = getRealPosition(fixedPos);
			posUpdate();
		}
	}

	
	public void moveDown()
	{
		if (moving)
		{
			IntVector2 fixedPos = getFixedPosition();

			if ((fixedPos.y) < (map.heigth - 1) && grid[fixedPos.x, fixedPos.y + 1] == null)
			{

				grid[fixedPos.x, fixedPos.y] = null;
				grid[fixedPos.x, fixedPos.y + 1] = this;
				fixedPos = new IntVector2(fixedPos.x, fixedPos.y + 1);
			}

			stopCheck(fixedPos);

			pos = getRealPosition(fixedPos);
			posUpdate();
		}
	}

	private void stopCheck(IntVector2 fixedPos)
	{ 
		if ((fixedPos.y) == (map.heigth - 1) || grid[fixedPos.x, fixedPos.y + 1] != null)
		{
			stop();
		}
	}

	private void posUpdate()
	{
		gameObject.transform.position = pos;
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
		IntVector2 fixedPos = getFixedPosition();
		Entity[,] currentGrid = map.getGrid();
		int counter = 0;

		for (int i = 0; i < 3; i++)
		{
			if ((fixedPos.y - (i + 1)) >= 0 && currentGrid[fixedPos.x, fixedPos.y - (i + 1)] is Coin)
			{
				++counter;
			}
		}

		if (counter == 3)
		{
			eraseCoinsAbove(fixedPos.x, fixedPos.y);
		}
	}

	private void eraseCoinsAbove(int x, int y)
	{
		Entity[,] currentGrid = map.getGrid();

		for (int i = 0; i < 4; i++)
		{
			GameObject toDestroy = currentGrid[x, y - i].getGameObject();
			Coin toDestroyCoin = toDestroy.GetComponent<Coin>();
			//if there is note
			if (toDestroyCoin.hasNote)
			{
				if(toDestroyCoin.note.getLeftCoin() == toDestroyCoin)
				{
					toDestroyCoin.note.getRightCoin().hasNote = false;
					toDestroyCoin.note.getRightCoin().note = null;
					toDestroyCoin.hasNote = false;

					map.getNotes().Remove(toDestroyCoin.note);
					Destroy(toDestroyCoin.note.gameObject);
					toDestroyCoin.note = null;
				}
				else
				{
					toDestroyCoin.note.getLeftCoin().hasNote = false;
					toDestroyCoin.note.getLeftCoin().note = null;
					toDestroyCoin.hasNote = false;

					map.getNotes().Remove(toDestroyCoin.note);
					Destroy(toDestroyCoin.note.gameObject);
					toDestroyCoin.note = null;
				}
			}
			////
			map.getCoins().Remove(toDestroyCoin);
			Destroy(toDestroy);
			currentGrid[x, y - i] = null;
		}
	}

	public void setNote(Note parentNote)
	{
		note = parentNote;
	}
}
