//using System.Collections;
//using System.Collections.Generic;

using UnityEngine;

public class MazeTileClass
{
    private bool west, south, //connections present?
                 draw, //should we draw this room? (also used in maze generation)
                 blocked, //used in generation to designate pre-generated rooms
                 flagged; //used in generation to designate cells that should be generated
    private MazeTileClass[,] mazeTile;

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

    public MazeTileClass[,] GenerateMaze(int mazeSize)
    {
        mazeTile = new MazeTileClass[mazeSize, mazeSize];

        for (int ly = 0; ly < mazeSize; ly++)
        {
            for (int lx = 0; lx < mazeSize; lx++)
            {
                mazeTile[lx, ly] = new MazeTileClass();
                mazeTile[lx, ly].SetDrawn(false);
            }
        }

        // Set Maze Default cells
        mazeTile[4, 3].SetDrawn(true); mazeTile[4, 3].SetBlocked(true);//These three tiles are empty with no connections. This helps hilight the middle cell as important
        mazeTile[3, 4].SetDrawn(true); mazeTile[3, 4].SetBlocked(true);
        mazeTile[5, 4].SetDrawn(true); mazeTile[5, 4].SetBlocked(true);

        mazeTile[4, 4].SetDrawn(true); // This is the middle tile. It has one connection to the south.
        mazeTile[4, 4].SetConnected("south", true);
        mazeTile[4, 4].SetBlocked(true);

        mazeTile[4, 5].SetDrawn(true); //This is the tile below the middle tile. It has connections to the north (set by the north tile), east (set by the east tile) and west.
        mazeTile[4, 5].SetBlocked(true);
        mazeTile[3, 5].SetConnected("east", true);
        mazeTile[4, 5].SetConnected("east", true);

        //set cells connected to default cells to flagged
        mazeTile[3, 5].SetFlagged(true);
        mazeTile[5, 5].SetFlagged(true);

        bool done = false, found;
        int x = 0, y = 0, north, east, south, west, heaviest;
        //Loop until done = true
        while (!done)
        {
            //find a tile that is Flagged.
            found = false;
            for (int sy = 0; sy < mazeSize; sy++)
            {
                for (int sx = 0; sx < mazeSize; sx++)
                {
                    if (mazeTile[sx, sy].IsFlagged()) { x = sx; y = sy; found = true; }
                }
            }
            for (int sy = 0; sy < mazeSize; sy++) //if no tiles are flagged, find one that has not been drawn.
            {
                for (int sx = 0; sx < mazeSize; sx++)
                {
                    if (!mazeTile[sx, sy].Drawn()) { x = sx; y = sy; found = true; }
                }
            }
            if (found) //if tiles are found then do this big hunk of code below:
            {
                mazeTile[x, y].SetFlagged(false); //unflag that tile
                mazeTile[x, y].SetDrawn(true);
                //generate random connections, then set the connected cells to be unblocked
                //I am using a weight system so I can generate multiple or single connections and integrate Bounding
                north = 2 + Random.Range(0, 5); east = 2 + Random.Range(0, 5); south = 2 + Random.Range(0, 5); west = 2 + Random.Range(0, 5); heaviest = 0; //establish random weights
                //weight a bit heavier to connect to cells with rooms
                if (y > 0 && mazeTile[x, y - 1].Drawn()) north += 2;
                if (x < mazeSize - 1 && mazeTile[x + 1, y].Drawn()) east += 2;
                if (y > mazeSize - 1 && mazeTile[x, y + 1].Drawn()) south += 2;
                if (x < 0 && mazeTile[x - 1, y].Drawn()) west += 2;

                if (y == 0) north = 0; if (y > 0 && mazeTile[x, y - 1].IsBlocked()) north = 0;                          //Establish bounds
                if (y == mazeSize - 1) south = 0; if (y < mazeSize - 1 && mazeTile[x, y + 1].IsBlocked()) south = 0;
                if (x == 0) west = 0; if (x > 0 && mazeTile[x - 1, y].IsBlocked()) west = 0;
                if (x == mazeSize - 1) east = 0; if (x < mazeSize - 1 && mazeTile[x + 1, y].IsBlocked()) east = 0;

                if (north > heaviest) heaviest = north; //Find the heaviest value
                if (east > heaviest) heaviest = east;
                if (south > heaviest) heaviest = south;
                if (west > heaviest) heaviest = west;

                if (heaviest == north)
                {
                    mazeTile[x, y - 1].SetConnected("south", true);
                    if (!mazeTile[x, y - 1].Drawn()) mazeTile[x, y - 1].SetFlagged(true);
                }
                if (heaviest == east)
                {
                    mazeTile[x, y].SetConnected("east", true);
                    if (!mazeTile[x + 1, y].Drawn()) mazeTile[x + 1, y].SetFlagged(true);
                }
                if (heaviest == south)
                {
                    mazeTile[x, y].SetConnected("south", true);
                    if (!mazeTile[x, y + 1].Drawn()) mazeTile[x, y + 1].SetFlagged(true);
                }
                if (heaviest == west)
                {
                    mazeTile[x - 1, y].SetConnected("east", true);
                    if (!mazeTile[x - 1, y].Drawn()) mazeTile[x - 1, y].SetFlagged(true);
                }
            }
            if (!found) done = true;//if nothing was found, your done!
        }
        return mazeTile;
    }
}
