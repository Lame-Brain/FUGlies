//using System.Collections;
//using System.Collections.Generic;

public class MazeTileClass
{
    private bool west, south, //connections present?
                 draw, //should we draw this room? (also used in maze generation)
                 blocked, //used in generation to designate pre-generated rooms
                 flagged; //used in generation to designate cells that should be generated

    public MazeTileClass()
    {
        west = false;
        south = false;
        draw = false;
        flagged = false;
    }

    public bool IsConnected(string dir)
    {
        bool c = false;
        if (dir == "east") c = west;
        if (dir == "south") c = south;
        return c;
    }
    public void SetConnected(string dir, bool tf)
    {
        if (dir == "east") west = tf;
        if (dir == "south") south = tf;
    }
    public void ToggleConnected(string dir)
    {
        if (dir == "east") west = !west;
        if (dir == "south") south = !south;
    }
    public bool Drawn()
    {
        return draw;
    }
    public void SetDrawn(bool tf)
    {
        draw = tf;
    }
    public void ToggleDrawn()
    {
        draw = !draw;
    }
    public bool IsBlocked()
    {
        return blocked;
    }
    public void SetBlocked(bool tf)
    {
        blocked = tf;
    }
    public bool IsFlagged()
    {
        return flagged;
    }
    public void SetFlagged(bool tf)
    {
        flagged = tf;
    }
    //866
    //207147977
    //503-591-7007-030217-5
}
