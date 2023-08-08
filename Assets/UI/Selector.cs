using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Selector
{
    public void SelectorOnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
		{
            if (Event.current.button == 0)
            {
                if (Event.current.clickCount == 1)
                {
                    this.dragBox.active = true;
                    this.dragBox.start = UI.MousePositionOnUI;
                }else if (Event.current.clickCount == 2)
                {
                    //Chọn toàn bộ cùng loại trong map khi ấn nút 2 lần
                }
            }
            if (Event.current.button == 1 && selected.Count > 0)
            {

            }
        }
        if (Event.current.type == EventType.MouseUp)
        {
            if (Event.current.button == 0)
            {
                this.dragBox.active = false;
                if(!dragBox.IsValid)
                {
                    //chọn 1 dưới con trỏ chuột
                }else
                {
                    //Get all in box
                    dragBox.startDrag = false;
                }
            }
        }
    }

    public DragBox dragBox = new DragBox();
    private List<object> selected = new List<object>();
}
