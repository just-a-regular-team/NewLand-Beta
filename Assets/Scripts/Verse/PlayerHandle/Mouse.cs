using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Mouse
{
    public Mouse()
    {
        _mouse = this;
    }

    public Vector3 GetMousePos
    {
        get{return currMousePos;}
    }

    private static Mouse _mouse;
    public static Mouse GetMouse
    {
        get
        {
            if(_mouse != null)
            {
                return _mouse;
            }
            Debug.LogError("Player pointer was not init yet");
            return null;
        }
    }
    public void MouseUpdate()
    {
        if(_mouse==null){return;}
        EscapeModeCheck();
        currMousePos = Current.Camera.ScreenToWorldPoint(Input.mousePosition);
        Current.Camera.orthographicSize = 5f;
        switch(mode)
        {
            case MouseMode.Normal:
            NormalUpdate();
            break;
            case MouseMode.ViewStragety:
            ViewStragetyUpdate();
            break;
            case MouseMode.Build:
            BuildUpdate();
            break;
            default:
            return;
        }
        //currMousePos.z = Current.CameraFollow.target.transform.position.z; not yet
    }

    void NormalUpdate()
    {
    }
    void ViewStragetyUpdate()
    {
        Current.Camera.orthographicSize = 15f;
    }
    void BuildUpdate()
    {

    }

    

    private Vector2 currMousePos;
    private Vector2 lastMousePos;

    private bool isDragging = false;

    #region  Mousemode 
    private MouseMode mode = MouseMode.Normal;
    public void ChangeModeTo(MouseMode mode)
    {
        this.mode = mode;
    }
    public bool IsMouseMode(MouseMode mode)
    {
        if(this.mode != mode)
        {
            return false;
        }
        return true;
    }
    public void EscapeModeCheck()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && (mode!=MouseMode.Normal))
        {
            ChangeModeTo(0);
        };
    }
    public enum MouseMode
    {
        Normal,
        ViewStragety,//Select or point army
        Build,
        
    }

    public static bool IsInputBlockedNow
    {
        get
        {
            WindowUI windowStack = Current.SceneRoot.uiRoot.windowUI;
            return  windowStack.MouseObscuredNow || !windowStack.CurrentWindowGetsInput; // || (mouseOverScrollViewStack && !mouseOverScrollViewStack.Peek());
        }
    }

    
    public static bool IsOver(Rect rect)
    {
        return rect.Contains(Event.current.mousePosition) && !Mouse.IsInputBlockedNow;
    }

    #endregion
    


     
     
}
