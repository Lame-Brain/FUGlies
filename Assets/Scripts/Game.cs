using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject floorTilePF, WallPF, NDoorPF, EDoorPF, SDoorPF, WDoorPF, hut, forge, alchemist, totem, sign, garden, meep;
    public Sprite[] floorTileTextures;
    [HideInInspector]
    public GameObject[,] roomTile;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate objects used to draw rooms
        roomTile = new GameObject[34, 34];
        for (int y = 0; y < 34; y++)
        {
            for (int x = 0; x < 34; x++)
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

    public void AddFUG(Vector3 pos)
    {
        GameObject go = Instantiate(meep, pos, Quaternion.identity);
        go.GetComponent<MeepClass>().roomX = GameManager.SAVE.focusX; go.GetComponent<MeepClass>().roomY = GameManager.SAVE.focusY;
        go.SetActive(false);
        GameManager.SAVE.meepList.Add(go);
    }

    public void DrawRoom(int tx, int ty)
    {
        //Draw the floors and walls
        for (int y = 0; y < GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY() + 1; y++)
        {
            for (int x = 0; x < GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() + 1; x++)
            {
                if (x == 0 || x == GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() || y == 0 || y == GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY()) //detects if we are drawing an edge, and draws a wall
                {
                    roomTile[x, y].GetComponent<SpriteRenderer>().sprite = WallPF.GetComponent<SpriteRenderer>().sprite;
                    roomTile[x, y].GetComponent<SpriteRenderer>().color = GameManager.SAVE.mazeTile[tx, ty].GetRoomColor();
                    roomTile[x, y].SetActive(true);
                }
                else //detects interior and draws floors
                {
                    roomTile[x, y].GetComponent<SpriteRenderer>().sprite = floorTileTextures[GameManager.SAVE.mazeTile[tx, ty].GetTileMap(x - 1, y - 1)];
                    roomTile[x, y].GetComponent<SpriteRenderer>().color = GameManager.SAVE.mazeTile[tx, ty].GetRoomColor();
                    roomTile[x, y].SetActive(true);
                }
            }
        }
        //Draw the doors
        if (ty > 0 && GameManager.SAVE.mazeTile[tx, ty - 1].IsConnected("south")) Instantiate(NDoorPF, new Vector3(GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() / 2, 0, 0), Quaternion.identity);
        if (GameManager.SAVE.mazeTile[tx, ty].IsConnected("south")) Instantiate(SDoorPF, new Vector3(GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() / 2, -GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY(), 0), Quaternion.identity);
        if (tx > 0 && GameManager.SAVE.mazeTile[tx - 1, ty].IsConnected("east")) Instantiate(WDoorPF, new Vector3(0, -GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY() / 2, 0), Quaternion.identity);
        if (GameManager.SAVE.mazeTile[tx, ty].IsConnected("east")) Instantiate(EDoorPF, new Vector3(GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX(), -GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY() / 2, 0), Quaternion.identity);

        //Draw the buildings
        for (int y = 0; y < GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeY() - 1; y++)
        {
            for (int x = 0; x < GameManager.SAVE.mazeTile[tx, ty].GetRoomSizeX() - 1; x++)
            {
                if (GameManager.SAVE.mazeTile[tx, ty].GetBuilding(x, y) == 1) { Instantiate(hut, new Vector3(x, -y, 0), Quaternion.identity); }
                if (GameManager.SAVE.mazeTile[tx, ty].GetBuilding(x, y) == 2) { Instantiate(forge, new Vector3(x, -y, 0), Quaternion.identity); }
                if (GameManager.SAVE.mazeTile[tx, ty].GetBuilding(x, y) == 3) { Instantiate(alchemist, new Vector3(x, -y, 0), Quaternion.identity); }
                if (GameManager.SAVE.mazeTile[tx, ty].GetBuilding(x, y) == 4) { Instantiate(totem, new Vector3(x, -y, 0), Quaternion.identity); }
                if (GameManager.SAVE.mazeTile[tx, ty].GetBuilding(x, y) == 5) { Instantiate(sign, new Vector3(x, -y, 0), Quaternion.identity); }
                if (GameManager.SAVE.mazeTile[tx, ty].GetBuilding(x, y) == 6) { Instantiate(garden, new Vector3(x, -y, 0), Quaternion.identity); }
            }
        }
        //Draw any meeps in the room
        foreach(GameObject go in GameManager.SAVE.meepList)
        {
            if (go.GetComponent<MeepClass>().roomX == tx && go.GetComponent<MeepClass>().roomY == ty)
            {
                go.SetActive(true);
            }
        }
    }
}