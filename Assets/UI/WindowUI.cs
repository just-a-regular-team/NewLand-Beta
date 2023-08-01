using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUI
{
    
    public void WindowUpdate()
    {
        UI_Update();
        for(int i = 0 ; i<this.windows.Count;i++)
        {
            windows[i].WindowUpdate();
        }
    }
    public virtual void DoWindowContents(Rect inRect){}
    public virtual void UI_Update()
    {

    }
    public virtual void PreOpen(){}
    public virtual void PostOpen(){}
    public virtual void PreClose(){}
    public virtual void PostClose(){}

    List<WindowUI> windows = new List<WindowUI>();
}
