using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Camera_Speed;
    private float zoom;

    // Start is called before the first frame update
    void Start()
    {
        zoom = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Camera.main.transform.position.x; float y = Camera.main.transform.position.y;

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) //Middle Mouse Button or Right Mouse Button
        {
            if (Input.GetAxis("Mouse X") < 0) x = x + Camera_Speed * 2f; //mouse moves left
            if (Input.GetAxis("Mouse X") > 0) x = x - Camera_Speed * 2f; //mouse moves right
            if (Input.GetAxis("Mouse Y") < 0) y = y + Camera_Speed * 2f; //mouse moves down
            if (Input.GetAxis("Mouse Y") > 0) y = y - Camera_Speed * 2f; //mouse moves up
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) zoom += 0.1f;
        if (Input.GetAxis("Mouse ScrollWheel") > 0) zoom -= 0.1f;

        //WASD or ArrowKeys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = y + Camera_Speed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = x - Camera_Speed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = y - Camera_Speed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = x + Camera_Speed;

        //bound camera movement
        if (x > 22) x = 22; if (x < -2) x = -2;
        if (y < -22) y = -22; if (y > 2) y = 2;
        if (zoom > 10) zoom = 10; if (zoom < 1) zoom = 1;

        Camera.main.transform.position = new Vector3(x, y, -10);
        Camera.main.orthographicSize = zoom;
    }
}
