using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIRoot
{
    // Start is called before the first frame update
    public virtual void UI_Init()
    {
        canvas.InitCanvas();
        windowUI.SetRoot(this);
    }

    // Update is called once per frame
    public virtual void UI_Update()
    {
        windowUI.WindowUpdate();
    }
    public virtual void UI_OnGUI()
    {
        windowUI.HandleEventsHighPriority();
    }
    public UICanvas canvas = new UICanvas();
    public WindowUI windowUI = new WindowUI();
}
