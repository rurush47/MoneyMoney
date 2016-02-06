using UnityEngine;
using System.Collections;

public class Piggy : Entity {

    void Awake()
    {
        _moving = false;
        Pos = gameObject.transform.position;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x / 2;
        Map = FindObjectOfType<Map>();
        Spawner = FindObjectOfType<Spawner>();
    }

    public void CoinCheck()
    {
        IntVector2 fixedPos = GetFixedPosition();
        Entity[,] currentGrid = Map.GetGrid();
        int counter1 = 0;
        int counter2 = 0;

        for (int i = 0; i < 4; i++)
        {
            Entity currenCoin = currentGrid[fixedPos.x, fixedPos.y - (i + 2)];
            if (currenCoin is Coin && currenCoin.GetComponent<Coin>().IsMoving() 
                && currenCoin.Type == Type)
            {
                ++counter1;
            }
        }

            
        for (int i = 0; i < 4; i++)
        {
            Entity currenCoin2 = currentGrid[fixedPos.x + 1, fixedPos.y - (i + 2)];
            if (currenCoin2 is Coin && currenCoin2.GetComponent<Coin>().IsMoving() 
                && currenCoin2.Type == Type)
            {
                ++counter2;
            }
        }

        if (counter1 == 4)
        {
            Score(fixedPos.x, fixedPos.y - 2);
        }

        if (counter2 == 4)
        {
            Score(fixedPos.x + 1, fixedPos.y - 2);
        }

    }

    public void Score(int x, int y)
    {
        Entity[,] currentGrid = Map.GetGrid();

        for(int i = 0; i < 4; i++)
        {
            GameObject toDestroy = currentGrid[x, y - i].GetGameObject();
            Map.GetCoins().Remove(toDestroy.GetComponent<Coin>());
            Destroy(toDestroy);
            currentGrid[x, y - i] = null;
        }
    }

}
