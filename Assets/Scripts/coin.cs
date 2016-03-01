using UnityEngine;
using System.Collections;

public class Coin : Entity
{
    public Note _note;
    public bool HasNote = false;
    public bool toBeErased = false;

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
        /*if (Input.GetKeyDown(KeyCode.A) && _moving && !HasNote)
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) && _moving && !HasNote)
        {
            MoveRight();
        }*/
    }

    public void MoveLeft()
    {
        if (_moving && !falling)
        {
            IntVector2 fixedPos = GetFixedPosition();

            if (fixedPos.x > 0 && Grid[fixedPos.x - 1, fixedPos.y] == null)
            {
                Grid[fixedPos.x, fixedPos.y] = null;
                Grid[fixedPos.x - 1, fixedPos.y] = this;
                fixedPos = new IntVector2(fixedPos.x - 1, fixedPos.y);
            }

            Pos = GetRealPosition(fixedPos);
            PosUpdate();
        }
    }

    public void MoveRight()
    {
        if (_moving && !falling)
        {
            IntVector2 fixedPos = GetFixedPosition();

            if (fixedPos.x < (Map.Width - 1) && Grid[fixedPos.x + 1, fixedPos.y] == null)
            {
                Grid[fixedPos.x, fixedPos.y] = null;
                Grid[fixedPos.x + 1, fixedPos.y] = this;
                fixedPos = new IntVector2(fixedPos.x + 1, fixedPos.y);
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

            if ((fixedPos.y) < (Map.Heigth - 1) && Grid[fixedPos.x, fixedPos.y + 1] == null)
            {
                Grid[fixedPos.x, fixedPos.y] = null;
                Grid[fixedPos.x, fixedPos.y + 1] = this;
                fixedPos = new IntVector2(fixedPos.x, fixedPos.y + 1);
            }
            //check if coin under is moving to prevent in-air blocking
            else if ((fixedPos.y) < (Map.Heigth - 1) && Grid[fixedPos.x, fixedPos.y + 1] != null
                && (Grid[fixedPos.x, fixedPos.y + 1]).GetComponent<Entity>().IsMoving())
            {
                Grid[fixedPos.x, fixedPos.y] = null;
                Grid[fixedPos.x, fixedPos.y + 1] = this;
                fixedPos = new IntVector2(fixedPos.x, fixedPos.y + 1);
            }

            StopCheck(fixedPos);

            Pos = GetRealPosition(fixedPos);
            PosUpdate();
        }
    }

    private void StopCheck(IntVector2 fixedPos)
    {
        //check if coin under is moving to prevent in-air blocking
        if ((fixedPos.y) == (Map.Heigth - 1) || (Grid[fixedPos.x, fixedPos.y + 1] != null
            && !(Grid[fixedPos.x, fixedPos.y + 1].GetComponent<Entity>().IsMoving())))
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
        falling = true;
    }

    public void CoinCheck()
    {
        IntVector2 fixedPos = GetFixedPosition();
        Entity[,] currentGrid = Map.GetGrid();
        int counter = 0;

        for (int i = 0; i < 3; i++)
        {
            if ((fixedPos.y - (i + 1)) >= 0 && currentGrid[fixedPos.x, fixedPos.y - (i + 1)] is Coin
                && currentGrid[fixedPos.x, fixedPos.y - (i + 1)].Type == Type)
            {
                ++counter;
            }
        }

        if (counter == 3)
        {
            for (int i = 0; i < 4; ++i)
            {
                currentGrid[fixedPos.x, fixedPos.y - i].GetComponent<Coin>().toBeErased = true;
            }
        }
    }

    public void SetNote(Note parentNote)
    {
        _note = parentNote;
    }

    public Note GetNote()
    {
        return _note;
    }

    public void RotateVertical()
    {
        Pos = Pos - new Vector2(Addition, -Addition);
        PosUpdate();
    }

    public void RotateHorizontal()
    {
        Pos = Pos - new Vector2(-Addition, Addition);
        PosUpdate();
    }
}