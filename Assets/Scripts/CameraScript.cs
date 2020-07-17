using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Camera_Speed;
    private float zoom;
    private float minBoundsX, minBoundsY, maxBoundsX, maxBoundsY, x, y;

    // Start is called before the first frame update
    void Start()
    {
        zoom = 10;         
    }

    // Update is called once per frame
    void Update()
    {
        //Camera_Speed should between 0.02 at zoom 1 and 0.1 at zoom 10
        Camera_Speed = ((zoom - 1) * .008f) + .02f;
        x = Camera.main.transform.position.x; y = Camera.main.transform.position.y;


        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) //Middle Mouse Button or Right Mouse Button
        {
            if (Input.GetAxis("Mouse X") < 0) x = x + Camera_Speed * 3f; //mouse moves left
            if (Input.GetAxis("Mouse X") > 0) x = x - Camera_Speed * 3f; //mouse moves right
            if (Input.GetAxis("Mouse Y") < 0) y = y + Camera_Speed * 3f; //mouse moves down
            if (Input.GetAxis("Mouse Y") > 0) y = y - Camera_Speed * 3f; //mouse moves up
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) zoom += 0.1f;
        if (Input.GetAxis("Mouse ScrollWheel") > 0) zoom -= 0.1f;

        //WASD or ArrowKeys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = y + Camera_Speed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = x - Camera_Speed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = y - Camera_Speed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = x + Camera_Speed;

        //bound camera movement
        //minBoundsX = 32 * GameManager.SAVE.focusX; minBoundsY = 32 * GameManager.SAVE.focusY;
        //maxBoundsX = minBoundsX + GameManager.SAVE.mazeTile[GameManager.SAVE.focusX, GameManager.SAVE.focusY].GetRoomSizeX();
        //maxBoundsY = minBoundsY + GameManager.SAVE.mazeTile[GameManager.SAVE.focusX, GameManager.SAVE.focusY].GetRoomSizeY();
        minBoundsX = 0; minBoundsY = 0;
        maxBoundsX = minBoundsX + GameManager.SAVE.mazeTile[GameManager.SAVE.focusX, GameManager.SAVE.focusY].GetRoomSizeX();
        maxBoundsY = minBoundsY + GameManager.SAVE.mazeTile[GameManager.SAVE.focusX, GameManager.SAVE.focusY].GetRoomSizeY();
        if (x > maxBoundsX) x = maxBoundsX; if (x < minBoundsX) x = minBoundsX;
        if (y < -maxBoundsY) y = -maxBoundsY; if (-y < minBoundsY) y = -minBoundsY;
        if (zoom > 10) zoom = 10; if (zoom < 1) zoom = 1;

        Camera.main.transform.position = new Vector3(x, y, -10);
        Camera.main.orthographicSize = zoom;
    }
}
