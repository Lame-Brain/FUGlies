using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!!!!!!!!!!!!!!!!!!!!!!!!!SANITY NOTE!!!!!!!!!!!!!!!!!
 * DoorLR always draw the tile to the Tile's RIGHT.
 * DoorUD always draw the tile to the Tile's BOTTOM.
 * that means DoorLR[0,0] and DoorUD[0,0] are valid,
 * but DoorLR[mazeSize, mazeSize] and DoorUD[mazeSize, mazeSize] are OUT OF RANGE!
 * Don't go crazy!
 */

public class Maze : MonoBehaviour
{
    public GameObject mazeTilePF, maze_UD_DoorPF, maze_LR_DoorPF;
    [HideInInspector]
    public MazeTileClass[,] mazeTile;
    [HideInInspector]
    public GameObject[,] tile, doorLR, doorUD;
    public int mazeSize;


    // Start is called before the first frame update
    void Start()
    {
        if (mazeSize == 0) mazeSize = 9;
        mazeTile = new MazeTileClass[mazeSize, mazeSize];
        tile = new GameObject[mazeSize, mazeSize];
        doorLR = new GameObject[mazeSize - 1, mazeSize - 1];
        doorUD = new GameObject[mazeSize - 1, mazeSize - 1];
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                tile[x, y] = Instantiate(mazeTilePF, new Vector3(x, y, 0), mazeTilePF.transform.rotation);
                tile[x, y].SetActive(false);                
            }
        } 
        for (int y = 0; y < mazeSize - 1; y++)
        {
            for (int x = 0; x < mazeSize - 1; x++)
            {
                doorLR[x, y] = Instantiate(maze_LR_DoorPF, new Vector3(x + 0.5f, y, 0), maze_LR_DoorPF.transform.rotation);
                doorLR[x, y].SetActive(false);
                doorUD[x, y] = Instantiate(maze_UD_DoorPF, new Vector3(x, y + 0.5f, 0), maze_UD_DoorPF.transform.rotation);
                doorUD[x, y].SetActive(false);
            }
        }
        GenerateMaze();
        DrawMaze();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawMaze()
    {
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                if(mazeTile[x,y].drawn()) tile[x, y].SetActive(true);
            }
        }
        for (int y = 0; y < mazeSize - 1; y++)
        {
            for (int x = 0; x < mazeSize - 1; x++)
            {
                if(mazeTile[x,y].IsConnected("east")) doorLR[x, y].SetActive(true);
                if (mazeTile[x, y].IsConnected("south")) doorUD[x, y].SetActive(true);
            }
        }
    }

    public void GenerateMaze()
    {

    }
}
