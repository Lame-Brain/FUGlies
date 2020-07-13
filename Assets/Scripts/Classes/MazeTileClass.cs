//using System.Collections;
//using System.Collections.Generic;

public class MazeTileClass
{
    private bool west, north, east, south;

    public MazeTileClass()
    {
        north = false;
        west = false; east = false;
        south = false;
    }

    public bool IsConnected(string dir)
    {
        bool c = false;
        if (dir == "west") c = west;
        if (dir == "north") c = north;
        if (dir == "east") c = east;
        if (dir == "south") c = south;
        return c;
    }
    public void SetConnected(string dir, bool tf)
    {
        if (dir == "west") west = tf;
        if (dir == "north") north = tf;
        if (dir == "east") east = tf;
        if (dir == "south") south = tf;
    }
    public void ToggleConnected(string dir)
    {
        if (dir == "west") west = !west;
        if (dir == "north") north = !north;
        if (dir == "east") east = !east;
        if (dir == "south") south = !south;
    }
    //866
    //207147977
    //503-591-7007-030217-5
}
