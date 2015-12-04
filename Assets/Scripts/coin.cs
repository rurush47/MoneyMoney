using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {
    private map grid;
    private Vector2 pos;
    private float time = 0;
    private float Addition;
    private bool moving = true;
    // Use this for initialization
    void Start () {
        pos = gameObject.transform.position;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x;
        grid = FindObjectOfType<map>();
    }
	
	// Update is called once per frame
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
        time += Time.deltaTime;
        if (time >= 1 && moving || Input.GetKey(KeyCode.S) && moving)
        {
            pos = getRealPosition(grid.moveDown(getFixedPosition(), this));
            Debug.Log(pos);
            posUpdate();
            time = 0;
        }
    }

    private void posUpdate()
    {
        gameObject.transform.position = pos;
    }

    public Vector2 getFixedPosition()
    {
        return new Vector2((int)(pos.x / Addition), -(int)(pos.y / Addition));
    }
    
    public Vector2 getRealPosition(Vector2 pos)
    {
        return new Vector2(pos.x, -pos.y) * Addition;
    }

    public void stop()
    {
        moving = false;
        Debug.Log("stahp");
    }
}
