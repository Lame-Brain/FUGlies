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
        //Initialize Maze tiles
        if (mazeSize == 0) mazeSize = 9;
        mazeTile = new MazeTileClass[mazeSize, mazeSize];
        tile = new GameObject[mazeSize, mazeSize];
        doorLR = new GameObject[mazeSize, mazeSize];
        doorUD = new GameObject[mazeSize, mazeSize];
        //mazeSize--; //This accounts for the array starting at 0;
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                mazeTile[x, y] = new MazeTileClass();
                mazeTile[x, y].SetDrawn(false);
                tile[x, y] = Instantiate(mazeTilePF, new Vector3(x, -y, 0), mazeTilePF.transform.rotation);
                tile[x, y].SetActive(false);                
            }
        } 
        for (int y = 0; y < mazeSize - 1; y++)
        {
            for (int x = 0; x < mazeSize - 1; x++)
            {
                doorLR[x, y] = Instantiate(maze_LR_DoorPF, new Vector3(x + 0.5f, -y, 0), maze_LR_DoorPF.transform.rotation);
                doorLR[x, y].SetActive(false);
                doorUD[x, y] = Instantiate(maze_UD_DoorPF, new Vector3(x, -y - 0.5f, 0), maze_UD_DoorPF.transform.rotation);
                doorUD[x, y].SetActive(false);
            }
        }
        for (int i = 0; i < mazeSize - 1; i++)
        {
            doorLR[i, mazeSize - 1] = Instantiate(maze_LR_DoorPF, new Vector3(i + 0.5f, -mazeSize + 1, 0), maze_LR_DoorPF.transform.rotation);
            doorLR[i, mazeSize - 1].SetActive(false);
            doorUD[mazeSize - 1, i] = Instantiate(maze_UD_DoorPF, new Vector3(mazeSize - 1, -i - 0.5f, 0), maze_UD_DoorPF.transform.rotation);
            doorUD[mazeSize - 1, i].SetActive(false);
        }

        //Debug.Log("X = " + i + ", Y = " + mazeSize + " and this shit is " + doorLR.Length);
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
        for (int i = 0; i < mazeSize - 1; i++)
        {
            if(mazeTile[i, mazeSize - 1].IsConnected("east")) doorLR[i, mazeSize - 1].SetActive(true);
            if (mazeTile[mazeSize - 1, i].IsConnected("south")) doorUD[mazeSize - 1, i].SetActive(true);
        }
    }

    public void GenerateMaze()
    {
        /*
        // Set Maze Defaults
        mazeTile[5, 4].SetDrawn(true); //These three tiles are empty with no connections. This helps hilight the middle cell as important
        mazeTile[4, 5].SetDrawn(true);
        mazeTile[6, 5].SetDrawn(true);

        mazeTile[5, 5].SetDrawn(true); // This is the middle tile. It has one connection to the south.
        mazeTile[5, 5].SetConnected("south", true);

        mazeTile[5, 6].SetDrawn(true); //This is the tile below the middle tile. It has connections to the north (set by the north tile), east (set by the east tile) and west.
        mazeTile[4, 6].SetConnected("east", true);
        mazeTile[5, 6].SetConnected("east", true); */

        //Generate from Upper-Left corner
        int x = 0, y = 0; bool done = false, found;
        int northWeight = 2, eastWeight = 2, southWeight = 2, westWeight = 2;
        int north, east, south, west, dir;
        while (!done)
        {
            //draw current tile
            Debug.Log("step 1, set drawn[" + x + ", " + y + "] to true");
            mazeTile[x, y].SetDrawn(true);
            
            //roll the dice on directions
            north = northWeight + Random.Range(0, 5); 
            east = eastWeight + Random.Range(0, 5);
            south = southWeight + Random.Range(0, 5);
            west = westWeight + Random.Range(0, 5);

            //  Bounds
            if (y == 0) north = 0;     
            if (y == mazeSize) south = 0;
            if (x == 0) west = 0;
            if (x == mazeSize) east = 0;

            //Find the heaviest
            dir = 0;
            if (north > dir) dir = north;
            if (south > dir) dir = south;
            if (east > dir) dir = east;
            if (west > dir) dir = west;

            //Debug.Log("North is " + north + ", East is " + east + ", South is " + south + ", West is " + west + ", and Dir is " + dir);
            if (dir == north) mazeTile[x, y - 1].SetConnected("south", true);
            if (dir == east) mazeTile[x, y].SetConnected("east", true);
            if (dir == south) mazeTile[x, y].SetConnected("south", true);
            if (dir == west) mazeTile[x - 1, y].SetConnected("east", true);
            
            //Debug.Log("step 2, set east and south connections for [" + x + ", " + y + "] to true");
            //Debug.Log("X : " + x + ", Y: " + y + " North = " + north + " East = " + east + " South = " + south + " West = " + west);
            //Debug.Log("X : " + x + ", Y: " + y + "[] South Door? " + mazeTile[x, y].IsConnected("south") + ", East Door? " + mazeTile[x, y].IsConnected("east"));

            //scan for next empty cell
            Debug.Log("Step 3, start While !Found loop");
            found = false; x = 0; y = 0;
            while (!found)
            {   
                if (mazeTile[x, y].drawn())
                {
                    Debug.Log("IF MazeTile at " + x + ", " + y + " is not found, then increment x to " + (x + 1));
                    x++;
                    if (x == mazeSize) { x = 0; y++; Debug.Log("if x equals Map Size, set X to " + x + " and increment Y to " + y); }
                    if (y == mazeSize) { found = true; done = true; Debug.Log("If Y equals Mapsize, found becomes True and we go back to step 1"); }
                }
                else
                {
                    found = true;
                }
            }
        }
    }
}
