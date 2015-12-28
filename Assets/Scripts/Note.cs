using UnityEngine;
using System.Collections;

public class Note : Entity {
    public GameObject coinPrefab;
    private Coin leftCoin;
    private Coin rightCoin;
    private bool moving = true;
    SpriteRenderer sprite;

    // Use this for initialization
    void Awake () {
		sprite = gameObject.GetComponent<SpriteRenderer>();
		pos = gameObject.transform.position;
		Addition = sprite.bounds.size.x / 2;
		map = FindObjectOfType<Map>();
        grid = map.getGrid();
		spawner = FindObjectOfType<Spawner>();
        //leftCoin = gameObject.transform.Find("coin1").gameObject.GetComponent<Coin>();
        GameObject leftCoinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity) as GameObject;
        leftCoin = leftCoinObj.GetComponent<Coin>();
        leftCoin.hasNote = true;
        leftCoin.setNote(this);
        //rightCoin = gameObject.transform.Find("coin2").gameObject.GetComponent<Coin>();
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

           /* Vector2 fixedPos = getFixedPosition();

            if ((int)fixedPos.x < (map.width - 2) && grid[(int)fixedPos.x + 2, (int)fixedPos.y] == null)
            {
                fixedPos = new Vector2(fixedPos.x + 1, fixedPos.y);
            }
            
            pos = getRealPosition(fixedPos);
            posUpdate();

        */
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
            sprite.transform.position = leftCoin.transform.position;
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

    public Coin getLeftCoin()
    {
        return leftCoin;
    }

    public Coin getRightCoin()
    {
        return rightCoin;
    }
}
