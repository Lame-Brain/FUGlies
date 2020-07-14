//using System.Collections;
//using System.Collections.Generic;

public class MazeTileClass
{
    private bool west, south, draw;

    public MazeTileClass()
    {
        west = false;
        south = false;
        draw = false;
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
    public bool drawn()
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
    //866
    //207147977
    //503-591-7007-030217-5
}
