﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MazeTileClass
{
    private bool west, south, //connections present?
                 draw, //should we draw this room? (also used in maze generation)
                 blocked, //used in generation to designate pre-generated rooms
                 flagged; //used in generation to designate cells that should be generated    
    private int[,] tile, building;
    private int roomSizeX, roomSizeY;
    private Color roomColor;    

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
    public void SetRoomSize(int x, int y)
    {
        roomSizeX = x; roomSizeY = y;
    }
    public int GetRoomSizeX()
    {
        return roomSizeX;
    }
    public int GetRoomSizeY()
    {
        return roomSizeY;
    }
    public void SetRoomColor(float r, float g, float b)
    {        
        roomColor = new Color(r / 255, g / 255, b / 255);        
    }
    public Color GetRoomColor()
    {
        return roomColor;
    }
    public int GetTileMap(int x, int y)
    {
        return tile[x,y];
    }
    public int GetBuilding(int x, int y)
    {
        return building[x, y];
    }
    public void MakeTileMap(int x, int y)
    {
        tile = new int[x, y];
        building = new int[x, y];
        for (int ly = 0; ly < y; ly++)
        {
            for(int lx = 0; lx < x; lx++)
            {
                tile[lx, ly] = Random.Range(0, 15);
                building[lx, ly] = 0;
            }
        }
    }
    public void updateBuilding(string bn, int x, int y)
    {
        int b = 0;
        if (bn == "hut") b = 1;
        if (bn == "forge") b = 2;
        if (bn == "alchemist") b = 3;
        if (bn == "totem") b = 4;
        if (bn == "sign") b = 5;
        if (bn == "garden") b = 6;
        building[x, y] = b;
    }
}
