using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton Static!
    public static GameManager MAIN;
    public static Save SAVE;
    public static Game GAME;

    public GameObject GameHolderObject;

    //Awake Function is First!
    void Awake()
    {
        if (MAIN == null) { MAIN = this; } else { Debug.Log("!!!!!!!WARNING: GAME MANAGER LOADED TWICE IN THIS SCENE!!!!!!!!!!!!"); }
        SAVE = new Save();
        GAME = GameHolderObject.GetComponent<Game>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Initialize maze
        SAVE.GenerateMaze(9);
        //TEST
        GAME.AddFUG(new Vector3(Random.Range(1f, 15f), -Random.Range(1f, 15f), 0));
        GAME.AddFUG(new Vector3(Random.Range(1f, 15f), -Random.Range(1f, 15f), 0));
        GAME.AddFUG(new Vector3(Random.Range(1f, 15f), -Random.Range(1f, 15f), 0));
        GAME.AddFUG(new Vector3(Random.Range(1f, 15f), -Random.Range(1f, 15f), 0));
        GAME.AddFUG(new Vector3(Random.Range(1f, 15f), -Random.Range(1f, 15f), 0));
        SAVE.mazeTile[SAVE.focusX, SAVE.focusY].updateBuilding("hut", 5, 5);
        GAME.DrawRoom(GameManager.SAVE.focusX, GameManager.SAVE.focusY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
