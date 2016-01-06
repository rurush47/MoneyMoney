using UnityEngine;
using System.Collections;

public class Piggy : Entity {

    void Awake()
    {
        pos = gameObject.transform.position;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x / 2;
        map = FindObjectOfType<Map>();
        spawner = FindObjectOfType<Spawner>();
    }

    public void coinCheck()
    {
        IntVector2 fixedPos = getFixedPosition();
        Entity[,] currentGrid = map.getGrid();
        int counter1 = 0;
        int counter2 = 0;

        for (int i = 0; i < 4; i++)
        {
            if(currentGrid[fixedPos.x, fixedPos.y - (i + 2)] is Coin)
            {
                ++counter1;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (currentGrid[fixedPos.x + 1, fixedPos.y - (i + 2)] is Coin)
            {
                ++counter2;
            }
        }

        if (counter1 == 4)
        {
            score(fixedPos.x, fixedPos.y - 2);
        }

        if (counter2 == 4)
        {
            score(fixedPos.x + 1, fixedPos.y - 2);
        }

    }

    public void score(int x, int y)
    {
        Entity[,] currentGrid = map.getGrid();

        for(int i = 0; i < 4; i++)
        {
            GameObject toDestroy = currentGrid[x, y - i].getGameObject();
            map.getCoins().Remove(toDestroy.GetComponent<Coin>());
            Destroy(toDestroy);
            currentGrid[x, y - i] = null;
        }
    }

}
