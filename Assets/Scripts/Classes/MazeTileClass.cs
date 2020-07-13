//using System.Collections;
//using System.Collections.Generic;

public class MazeTileClass
{
    private bool west, north, east, south, draw;

    public MazeTileClass()
    {
        east = false;
        south = false;
        draw = false;
    }

    public bool IsConnected(string dir)
    {
        bool c = false;
        if (dir == "east") c = east;
        if (dir == "south") c = south;
        return c;
    }
    public void SetConnected(string dir, bool tf)
    {
        if (dir == "east") east = tf;
        if (dir == "south") south = tf;
    }
    public void ToggleConnected(string dir)
    {
        if (dir == "east") east = !east;
        if (dir == "south") south = !south;
    }
    public bool drawn()
    {
        return draw;
    }
    public void SetTile(bool tf)
    {
        draw = tf;
    }
    public void ToggleTile()
    {
        draw = !draw;
    }
    //866
    //207147977
    //503-591-7007-030217-5
}
