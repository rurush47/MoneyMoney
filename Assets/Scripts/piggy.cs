using UnityEngine;
using System.Collections;

public class Piggy : Entity {

    public Spawner spawner;
    private Map grid;
    private Vector2 pos;
    private float Addition;
    

    void Awake()
    {
        pos = gameObject.transform.position;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x / 2;
        grid = FindObjectOfType<Map>();
        spawner = FindObjectOfType<Spawner>();
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

    public void coinCheck()
    {
        Vector2 fixedPos = getFixedPosition();
        Entity[,] currentGrid = grid.getGrid();
        int counter1 = 0;
        int counter2 = 0;

        for (int i = 0; i < 4; i++)
        {
            if(currentGrid[(int)fixedPos.x, (int)fixedPos.y - (i + 2)] is Coin)
            {
                ++counter1;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (currentGrid[(int)fixedPos.x + 1, (int)fixedPos.y - (i + 2)] is Coin)
            {
                ++counter2;
            }
        }

        if (counter1 == 4)
        {
            score((int)fixedPos.x, (int)fixedPos.y - 2);
        }

        if (counter2 == 4)
        {
            score((int)fixedPos.x + 1, (int)fixedPos.y - 2);
        }

    }

    public void score(int x, int y)
    {
        Entity[,] currentGrid = grid.getGrid();

        for(int i = 0; i < 4; i++)
        {
            GameObject toDestroy = currentGrid[x, y - i].getGameObject();
            grid.getCoins().Remove(toDestroy.GetComponent<Coin>());
            Destroy(toDestroy);
            currentGrid[x, y - i] = null;
        }
    }

}
