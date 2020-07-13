using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        doorLR = new GameObject[mazeSize, mazeSize];
        doorUD = new GameObject[mazeSize, mazeSize];
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                tile[x, y] = Instantiate(mazeTilePF, new Vector3(x, y, 0), mazeTilePF.transform.rotation);
            }
        } 
        for (int y = 0; y < mazeSize - 1; y++)
        {
            for (int x = 0; x < mazeSize - 1; x++)
            {
                //doorLR[x, y] = Instantiate(mazeTilePF, new Vector3(x, y, 0), mazeTilePF.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
