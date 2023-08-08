using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlaying : UIRoot
{
    // Start is called before the first frame update
    public override void UI_Init()
    {
        base.UI_Init();
    }

    // Update is called once per frame
    public override void UI_Update()
    {
        base.UI_Update();
    }

    public override void UI_OnGUI()
    {
        base.UI_OnGUI();
        windowUI.WindowONGUI();
        if(Mouse.GetMouse.IsMouseMode(Mouse.MouseMode.ViewStragety))
        {
            selector.dragBox.DragBoxOnGUI();
            selector.SelectorOnGUI();
        }
    }

    public Selector selector = new Selector();
}
