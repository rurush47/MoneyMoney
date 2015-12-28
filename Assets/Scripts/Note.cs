using UnityEngine;
using System.Collections;

public class Note : Entity {
    private Coin leftCoin;
    private Coin rightCoin;
    private bool moving = true;

	// Use this for initialization
	void Awake () {
		pos = gameObject.transform.position;
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		Addition = sprite.bounds.size.x / 2;
		map = FindObjectOfType<Map>();
        grid = map.getGrid();
		spawner = FindObjectOfType<Spawner>();
        leftCoin = gameObject.transform.Find("coin1").gameObject.GetComponent<Coin>();
        leftCoin.hasNote = true;
        leftCoin.setNote(this);
        rightCoin = gameObject.transform.Find("coin2").gameObject.GetComponent<Coin>();
        rightCoin.hasNote = true;
        rightCoin.setNote(this);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A) && moving)
        {
            moveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) && moving)
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
                fixedPos = new Vector2(fixedPos.x - 1, fixedPos.y);
            }

            pos = getRealPosition(fixedPos);
            posUpdate();
            //
            leftCoin.moveLeft();
            rightCoin.moveLeft();
        }
    }

    public void moveRight()
    {
        if(moving)
        {

            Vector2 fixedPos = getFixedPosition();

            if ((int)fixedPos.x < (map.width - 2) && grid[(int)fixedPos.x + 2, (int)fixedPos.y] == null)
            {
                fixedPos = new Vector2(fixedPos.x + 1, fixedPos.y);
            }
            
            pos = getRealPosition(fixedPos);
            posUpdate();

        
            rightCoin.moveRight();
            leftCoin.moveRight();
        }
    }


    public void moveDown()
    {
        /*if (moving)
        {
            Vector2 fixedPos = getFixedPosition();

            if ((fixedPos.y + 1) == (map.heigth - 1) || grid[(int)fixedPos.x, (int)fixedPos.y + 2] != null
                || grid[(int)fixedPos.x + 1, (int)fixedPos.y + 2] != null)
            {
                stop();
                leftCoin.moveDown();
                leftCoin.stop();
                rightCoin.moveDown();
                rightCoin.stop();
                fixedPos = new Vector2(fixedPos.x, fixedPos.y + 1);
            }
            else
            {
                rightCoin.moveDown();
                leftCoin.moveDown();
                fixedPos = new Vector2(fixedPos.x, fixedPos.y + 1);
            }

            pos = getRealPosition(fixedPos);
            posUpdate();
        }*/
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

    public Coin getLeftCoin()
    {
        return leftCoin;
    }

    public Coin getRightCoin()
    {
        return rightCoin;
    }
}
