using UnityEngine;
using System.Collections;

public class Note : Entity {
    public GameObject coinPrefab;
    private Coin leftCoin;
    private Coin rightCoin;
    private bool moving = true;

    // Use this for initialization
    void Awake () {
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		pos = gameObject.transform.position;
		Addition = sprite.bounds.size.x / 2;
		map = FindObjectOfType<Map>();
        grid = map.getGrid();
		spawner = FindObjectOfType<Spawner>();
        //leftCoin
        GameObject leftCoinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity) as GameObject;
        leftCoin = leftCoinObj.GetComponent<Coin>();
        leftCoin.hasNote = true;
        leftCoin.setNote(this);
        //rightCoin
        GameObject rightCoinObj = Instantiate(coinPrefab, transform.position + new Vector3(1*Addition, 0, 0), Quaternion.identity) as GameObject;
        rightCoin = rightCoinObj.GetComponent<Coin>();
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

        if (Input.GetKeyDown(KeyCode.Space) && moving)
        {
            rotate();
        }
    }

    public void moveLeft()
    {
        if(moving)
        { 
            leftCoin.moveLeft();
            rightCoin.moveLeft();
            transform.position = leftCoin.transform.position;
            
        }
    }

    public void moveRight()
    {
        if(moving)
        { 
            rightCoin.moveRight();
            leftCoin.moveRight();
            transform.position = leftCoin.transform.position;
        }
    }


    public void moveDown()
    {
       if (moving)
        {
            if (!leftCoin.isMoving())
            {
                rightCoin.stop();
            }
            if (!rightCoin.isMoving())
            {
                leftCoin.stop();
            }
            transform.position = leftCoin.transform.position;
        }
    }

    public void rotate()
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

    public Coin getLeftCoin()
    {
        return leftCoin;
    }

    public Coin getRightCoin()
    {
        return rightCoin;
    }
}
