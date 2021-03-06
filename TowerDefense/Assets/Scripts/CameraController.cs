﻿//Handles camera movement, allowing a player to move the camera around or zoom in and out (might remove).
using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool doMovement = true;

    public float panSpeed = 30f;
    public float panBoarderThickness = 10f;
    public float scrollSpeed = 5f;

    public float minY = 10f;
    public float maxY = 80f;

	void Update () {
        //Escape key toggles the ability to move the camera.
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;
        //General movement is by pushing a key or by moving the mouse to an edge of the screen.
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x>= Screen.width - panBoarderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        //Zoom in and out via scroll wheel.
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }
}
