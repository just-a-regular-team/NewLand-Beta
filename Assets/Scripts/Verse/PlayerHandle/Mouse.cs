using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse
{
    public Mouse()
    {
        GetMouse = this;
    }
    public static Mouse GetMouse;
    private void MouseUpdate()
    {
        if(GetMouse==null){return;}

        currMousePos = Current.Camera.ScreenToWorldPoint(Input.mousePosition);
        //currMousePos.z = Current.CameraFollow.target.transform.position.z; not yet
    }


    private Vector2 currMousePos;
    private Vector2 lastMousePos;

    private bool isDragging = false;


    public Vector3 GetMousePos
    {
        get{return currMousePos;}
    }
    public enum MouseMode
    {
        Select,
        Build,
        
    }
}
