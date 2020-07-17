using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour    
{
    public GameObject floorTilePF, WallPF, NDoorPF, EDoorPF, SDoorPF, WDoorPF;    
    public Sprite[] floorTileTextures;
    [HideInInspector]
    public GameObject[,] roomTile;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate objects used to draw rooms
        roomTile = new GameObject[34, 34];
        for(int y = 0; y < 34; y++)
        {
            for(int x = 0; x < 34; x++)
            {
                roomTile[x, y] = Instantiate(floorTilePF, new Vector3(x, -y, 0), Quaternion.identity);
                roomTile[x, y].SetActive(false);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawRoom(int tx, int ty)
    {
        Debug.Log("I got X = " + tx + ", Y = " + ty);
        /*Debug.Log(GameManager.SAVE.mazeTile[0, 0].GetTileMap(0, 0) + " " + GameManager.SAVE.mazeTile[0, 0].GetTileMap(1, 0) + " " + GameManager.SAVE.mazeTile[0, 0].GetTileMap(2, 0) + " " + 
            GameManager.SAVE.mazeTile[0, 0].GetTileMap(3, 0) + " " + GameManager.SAVE.mazeTile[0, 0].GetTileMap(4, 0) + " " + GameManager.SAVE.mazeTile[0, 0].GetTileMap(5, 0) + " " + 
            GameManager.SAVE.mazeTile[0, 0].GetTileMap(6, 0) + " " + GameManager.SAVE.mazeTile[0, 0].GetTileMap(7, 0) + " " + GameManager.SAVE.mazeTile[0, 0].GetTileMap(8, 0) + " " + 
            GameManager.SAVE.mazeTile[0, 0].GetTileMap(9, 0) + GameManager.SAVE.mazeTile[0, 0].GetTileMap(10, 0)); */
        Debug.Log("North Door? " + GameManager.SAVE.mazeTile[tx, ty - 1].IsConnected("south") + "\n");
        Debug.Log("East Door? " + GameManager.SAVE.mazeTile[tx, ty].IsConnected("east") + "\n");
        Debug.Log("South Door? " + GameManager.SAVE.mazeTile[tx, ty].IsConnected("south") + "\n");
        Debug.Log("West Door? " + GameManager.SAVE.mazeTile[tx - 1, ty].IsConnected("east") + "\n");
        Debug.Log("Test Test \n Test");
        
        //int tx = GameManager.SAVE.focusX, ty = GameManager.SAVE.focusY;

        for (int y = 0; y < GameManager.SAVE.mazeTile[tx,ty].GetRoomSizeY()+1; y++)
        {
            for (int x = 0; x < GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX()+1; x++)
            {
                if(x == 0 || x == GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() || y == 0 || y == GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY()) //detects if we are drawing an edge, and draws a wall
                {
                    roomTile[x, y].GetComponent<SpriteRenderer>().sprite = WallPF.GetComponent<SpriteRenderer>().sprite;
                    roomTile[x, y].GetComponent<SpriteRenderer>().color = GameManager.SAVE.mazeTile[tx, ty].GetRoomColor();
                    roomTile[x, y].SetActive(true);
                }
                else //detects interior and draws floors
                {
                    roomTile[x, y].GetComponent<SpriteRenderer>().sprite = floorTileTextures[GameManager.SAVE.mazeTile[tx,ty].GetTileMap(x -1,y -1)];
                    roomTile[x, y].GetComponent<SpriteRenderer>().color = GameManager.SAVE.mazeTile[tx, ty].GetRoomColor();
                    roomTile[x, y].SetActive(true);
                }
            }
        }
        
        if (ty > 0 && GameManager.SAVE.mazeTile[tx, ty - 1].IsConnected("south")) Instantiate(NDoorPF, new Vector3(GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() / 2, 0, 0), Quaternion.identity);
        if (GameManager.SAVE.mazeTile[tx, ty].IsConnected("south")) Instantiate(SDoorPF, new Vector3(GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() / 2, -GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY(), 0), Quaternion.identity);
        if (tx > 0 && GameManager.SAVE.mazeTile[tx - 1, ty].IsConnected("east")) Instantiate(WDoorPF, new Vector3(0, -GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY() / 2, 0), Quaternion.identity);
        if (GameManager.SAVE.mazeTile[tx, ty].IsConnected("east")) Instantiate(EDoorPF, new Vector3(GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX(), -GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY() / 2, 0), Quaternion.identity);

    }
}
