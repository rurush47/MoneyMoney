﻿using UnityEngine;
using System.Collections;

public class Note : Entity {
	public GameObject CoinPrefab;
	private Coin _leftCoin;
	private Coin _rightCoin;
    public bool _isVertical = false;

	// Use this for initialization
	void Awake () {
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		Pos = gameObject.transform.position;
		Addition = sprite.bounds.size.x / 2;
		Map = FindObjectOfType<Map>();
		Grid = Map.GetGrid();
		Spawner = FindObjectOfType<Spawner>();
		//leftCoin
		GameObject leftCoinObj = Instantiate(CoinPrefab, transform.position, Quaternion.identity) as GameObject;
		_leftCoin = leftCoinObj.GetComponent<Coin>();
		_leftCoin.HasNote = true;
		_leftCoin.SetNote(this);
		//rightCoin
		GameObject rightCoinObj = Instantiate(CoinPrefab, transform.position + new Vector3(1*Addition, 0, 0), Quaternion.identity) as GameObject;
		_rightCoin = rightCoinObj.GetComponent<Coin>();
		_rightCoin.HasNote = true;
		_rightCoin.SetNote(this);
	}
	
	// Update is called once per frame
	void Update ()
    { 

	}

	public void MoveLeft()
	{
		if(_moving && !falling)
		{ 
            if (_isVertical)
            {
                if(_leftCoin.CanMoveLeft() && _rightCoin.CanMoveLeft())
                {
                    _leftCoin.MoveLeft();
                    _rightCoin.MoveLeft();
                    transform.position = _leftCoin.transform.position;
                }
            }
            else
            {
			    _leftCoin.MoveLeft();
			    _rightCoin.MoveLeft();
			    transform.position = _leftCoin.transform.position;
            }
		}
	}

	public void MoveRight()
	{
		if(_moving && !falling)
		{ 
            if(_isVertical)
            {
                if(_leftCoin.CanMoveRight() && _rightCoin.CanMoveRight())
                {
                    _rightCoin.MoveRight();
                    _leftCoin.MoveRight();
                    transform.position = _leftCoin.transform.position;
                }
            }
            else
            {
			    _rightCoin.MoveRight();
			    _leftCoin.MoveRight();
			    transform.position = _leftCoin.transform.position;
            }
		}
	}


	public void MoveDown()
	{
	   if (_moving)
		{
			if (!_leftCoin.IsMoving())
			{
				_rightCoin.Stop();
                Stop();
			}
			if (!_rightCoin.IsMoving())
			{
				_leftCoin.Stop();
                Stop();
            }
            transform.position = _leftCoin.transform.position;
		}
	}

	public void Rotate()
	{
        IntVector2 fixedPos = _rightCoin.GetFixedPosition();
        IntVector2 fixedPosLeft = _leftCoin.GetFixedPosition();
        Entity[,] grid = Map.GetGrid();
                                
        if (fixedPos.y > 0 && fixedPosLeft.x < Map.Width - 1)
        {

            if (_isVertical)
            {
                _isVertical = false;
                //change coin pos on map
                grid[fixedPos.x, fixedPos.y] = null;
                grid[fixedPos.x + 1, fixedPos.y + 1] = _rightCoin;

                _rightCoin.RotateHorizontal();
                //change sprite
                string type = Map.TypeName(Type);
                Sprite newSprite = Resources.LoadAll<Sprite>("GfX/" + type + "Spreadsheet")[1];
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            }
            else
            {
                _isVertical = true;
                //change coin pos on map
                grid[fixedPos.x, fixedPos.y] = null;
                grid[fixedPos.x - 1, fixedPos.y - 1] = _rightCoin;

                _rightCoin.RotateVertical();
                //change sprite
                string type = Map.TypeName(Type);
                Sprite newSprite = Resources.LoadAll<Sprite>("GfX/" + type + "Spreadsheet")[2];
                gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            }
        }
	}

	public void Stop()
	{
		_moving = false;
	}

	private void PosUpdate()
	{
		gameObject.transform.position = Pos;
	}

	public Coin GetLeftCoin()
	{
		return _leftCoin;
	}

	public Coin GetRightCoin()
	{
		return _rightCoin;
	}

    public bool CanMoveDown()
    {
        IntVector2 coinPos = _leftCoin.GetFixedPosition();
        IntVector2 coin2Pos = _rightCoin.GetFixedPosition();

        if (Map.CanMoveDown(coinPos.x, coinPos.y) && Map.CanMoveDown(coin2Pos.x, coin2Pos.y))
        {
            return true;
        }
        else
            return false;
    }

}
