using UnityEngine;
using System.Collections;

public class Coin : Entity
{
    private Note _note;
    private bool _moving = true;
    public bool HasNote = false;


    void Awake()
    {
        Pos = gameObject.transform.position;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Addition = sprite.bounds.size.x;
        Map = FindObjectOfType<Map>();
        Grid = Map.GetGrid();
        Spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _moving && !HasNote)
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) && _moving && !HasNote)
        {
            MoveRight();
        }
    }

    public void MoveLeft()
    {
        if (_moving)
        {
            IntVector2 fixedPos = GetFixedPosition();

            if (fixedPos.X > 0 && Grid[fixedPos.X - 1, fixedPos.Y] == null)
            {
                Grid[fixedPos.X, fixedPos.Y] = null;
                Grid[fixedPos.X - 1, fixedPos.Y] = this;
                fixedPos = new IntVector2(fixedPos.X - 1, fixedPos.Y);
            }

            Pos = GetRealPosition(fixedPos);
            PosUpdate();
        }
    }

    public void MoveRight()
    {
        if (_moving)
        {
            IntVector2 fixedPos = GetFixedPosition();

            if (fixedPos.X < (Map.Width - 1) && Grid[fixedPos.X + 1, fixedPos.Y] == null)
            {
                Grid[fixedPos.X, fixedPos.Y] = null;
                Grid[fixedPos.X + 1, fixedPos.Y] = this;
                fixedPos = new IntVector2(fixedPos.X + 1, fixedPos.Y);
            }

            Pos = GetRealPosition(fixedPos);
            PosUpdate();
        }
    }


    public void MoveDown()
    {
        if (_moving)
        {
            IntVector2 fixedPos = GetFixedPosition();

            if ((fixedPos.Y) < (Map.Heigth - 1) && Grid[fixedPos.X, fixedPos.Y + 1] == null)
            {
                Grid[fixedPos.X, fixedPos.Y] = null;
                Grid[fixedPos.X, fixedPos.Y + 1] = this;
                fixedPos = new IntVector2(fixedPos.X, fixedPos.Y + 1);
            }

            StopCheck(fixedPos);

            Pos = GetRealPosition(fixedPos);
            PosUpdate();
        }
    }

    private void StopCheck(IntVector2 fixedPos)
    {
        if ((fixedPos.Y) == (Map.Heigth - 1) || Grid[fixedPos.X, fixedPos.Y + 1] != null)
        {
            Stop();
        }
    }

    private void PosUpdate()
    {
        gameObject.transform.position = Pos;
    }

    public void Stop()
    {
        _moving = false;
    }

    public bool IsMoving()
    {
        return _moving;
    }

    public void CoinCheck()
    {
        IntVector2 fixedPos = GetFixedPosition();
        Entity[,] currentGrid = Map.GetGrid();
        int counter = 0;

        for (int i = 0; i < 3; i++)
        {
            if ((fixedPos.Y - (i + 1)) >= 0 && currentGrid[fixedPos.X, fixedPos.Y - (i + 1)] is Coin
                && currentGrid[fixedPos.X, fixedPos.Y - (i + 1)].Type == Type)
            {
                ++counter;
            }
        }

        if (counter == 3)
        {
            EraseCoinsAbove(fixedPos.X, fixedPos.Y);
        }
    }

    private void EraseCoinsAbove(int x, int y)
    {
        Entity[,] currentGrid = Map.GetGrid();

        for (int i = 0; i < 4; i++)
        {
            GameObject toDestroy = currentGrid[x, y - i].GetGameObject();
            Coin toDestroyCoin = toDestroy.GetComponent<Coin>();
            //if there is note
            if (toDestroyCoin.HasNote)
            {
                if (toDestroyCoin._note.GetLeftCoin() == toDestroyCoin)
                {
                    toDestroyCoin._note.GetRightCoin().HasNote = false;
                    toDestroyCoin._note.GetRightCoin()._note = null;
                    toDestroyCoin.HasNote = false;

                    Map.GetNotes().Remove(toDestroyCoin._note);
                    Destroy(toDestroyCoin._note.gameObject);
                    toDestroyCoin._note = null;
                }
                else
                {
                    toDestroyCoin._note.GetLeftCoin().HasNote = false;
                    toDestroyCoin._note.GetLeftCoin()._note = null;
                    toDestroyCoin.HasNote = false;

                    Map.GetNotes().Remove(toDestroyCoin._note);
                    Destroy(toDestroyCoin._note.gameObject);
                    toDestroyCoin._note = null;
                }
            }
            ////
            Map.GetCoins().Remove(toDestroyCoin);
            Destroy(toDestroy);
            currentGrid[x, y - i] = null;
        }
    }

    public void SetNote(Note parentNote)
    {
        _note = parentNote;
    }
}