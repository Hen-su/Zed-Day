using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public Vector3 shootDir;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private Vector3 GetPlayerDirection()
    {
        //Player Rotation with mouse
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylength;
        if (groundPlane.Raycast(cameraRay, out raylength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylength);
            return new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
        }
        return new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerDirection();
    }
}
