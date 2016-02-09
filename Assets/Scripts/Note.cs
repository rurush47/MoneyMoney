using UnityEngine;
using System.Collections;

public class Note : Entity {
	public GameObject CoinPrefab;
	private Coin _leftCoin;
	private Coin _rightCoin;
	private bool _moving = true;

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
	void Update () {
		if (Input.GetKeyDown(KeyCode.A) && _moving)
		{
			MoveLeft();
		}

		if (Input.GetKeyDown(KeyCode.D) && _moving)
		{
			MoveRight();
		}

		if (Input.GetKeyDown(KeyCode.Space) && _moving)
		{
			Rotate();
		}
	}

	public void MoveLeft()
	{
		if(_moving && !falling)
		{ 
			_leftCoin.MoveLeft();
			_rightCoin.MoveLeft();
			transform.position = _leftCoin.transform.position;
			
		}
	}

	public void MoveRight()
	{
		if(_moving && !falling)
		{ 
			_rightCoin.MoveRight();
			_leftCoin.MoveRight();
			transform.position = _leftCoin.transform.position;
		}
	}


	public void MoveDown()
	{
	   if (_moving)
		{
			if (!_leftCoin.IsMoving())
			{
				_rightCoin.Stop();
			}
			if (!_rightCoin.IsMoving())
			{
				_leftCoin.Stop();
			}
			transform.position = _leftCoin.transform.position;
		}
	}

	public void Rotate()
	{

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
}
