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
                if(mazeTile[x,y].Drawn()) tile[x, y].SetActive(true);
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
                if (x < mazeSize -1 && mazeTile[x + 1, y].Drawn()) east += 2;
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
    }
}
/* One possible algorithim:
 * 1. Pick any random cell in the grid that is not blocked
 * 2. Find a random neighboring cell that hasn't been visited yet.
 * 3. If you find one, strip the wall between the current cell and the neighboring cell.
 * 4. If you don't find one,  return to the previous cell.
 * 5. Repeat steps 2 and 3  (or steps 2 and 4)  for every cell in the grid. 
 */
