using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUI
{
    public void SetParent(WindowUI parent)
    {
        this.parent = parent;
    }
    public void SetRoot(UIRoot uiRoot)
    {
        this.uiRoot = uiRoot;
    }
     

    public virtual void DoWindowContents(Rect inRect){}
    public virtual void UI_Update(){}
    public virtual void WindowOnGUI(){}
    public virtual void PreOpen(){}
    public virtual void PostOpen(){}
    public virtual void PreClose(){}
    public virtual void PostClose(){}
    public virtual void OnCancelKeyPressed()
    {
        if (this.closeOnCancel)
        {
            this.Close(true);
            Event.current.Use();
        }
    }

    // Token: 0x06002753 RID: 10067 RVA: 0x000FCCFD File Offset: 0x000FAEFD
    public virtual void OnAcceptKeyPressed()
    {
        if (this.closeOnAccept)
        {
            this.Close(true);
            Event.current.Use();
        }
    }

    public virtual void Close(bool doCloseSound = true)
    {
        uiRoot.windowUI.TryRemove(this, doCloseSound);
    }

    public void WindowUpdate()
    {
        UI_Update();
        for(int i = 0 ; i<this.windows.Count;i++)
        {
            windows[i].WindowUpdate();
        }
    }
    public void HandleEventsHighPriority()
    {
        if (KeyBindingOf.Cancel.GetKeyDown)
        {
            this.Notify_PressedCancel();
        }
        if (KeyBindingOf.Accept.GetKeyDown)
        {
            this.Notify_PressedAccept();
        }
        if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.KeyDown) && !this.GetsInput(null))
        {
            Event.current.Use();
        }
    }


    public bool IsOpen(Type type)
    {
        for (int i = 0; i < this.windows.Count; i++)
        {
            if (this.windows[i].GetType() == type)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsOpen()
    {
        return IsOpen(this);
    }
    public bool IsOpen(WindowUI window)
	{
		return this.windows.Contains(window);
	}
    public bool GetsInput(WindowUI window)
    {
        if (this == window)
        {
            return true;
        }
        if (this.absorbInputAroundWindow)
        {
            return false;
        }
        return true;
    }
    
    public bool GetsInputFrom(WindowUI window)
    {
        for (int i = this.windows.Count - 1; i >= 0; i--)
        {
            if (this.windows[i] == window)
            {
                return true;
            }
            if (this.windows[i].absorbInputAroundWindow)
            {
                return false;
            }
        }
        return true;
    }
    public void Notify_PressedCancel()
    {
        for (int i = parent.windows.Count - 1; i >= 0; i--)
        {
            if ((parent.windows[i].closeOnCancel || parent.windows[i].forceCatchAcceptAndCancelEventEvenIfUnfocused) && parent.GetsInput(parent.windows[i]))
            {
                parent.windows[i].OnCancelKeyPressed();
                return;
            }
        }
    }

    // Token: 0x0600276D RID: 10093 RVA: 0x000FD420 File Offset: 0x000FB620
    public void Notify_PressedAccept()
    {
        for (int i = parent.windows.Count - 1; i >= 0; i--)
        {
            if ((parent.windows[i].closeOnAccept || parent.windows[i].forceCatchAcceptAndCancelEventEvenIfUnfocused) && parent.GetsInput(parent.windows[i]))
            {
                parent.windows[i].OnAcceptKeyPressed();
                return;
            }
        }
    }
    
    public bool TryRemove()
    {
        if(parent == null && uiRoot != null)
        {
            uiRoot.windowUI.PreClose();
            uiRoot.windowUI.windows.Clear();
            uiRoot.windowUI.PostClose();
            return true;
        }
        TryRemove(parent,false);
        return true;
    }
    public bool TryRemove(WindowUI window, bool doCloseSound = true)
    {
        bool flag = false;
        for (int i = 0; i < window.windows.Count; i++)
        {
            if (window.windows[i] == this)
            {
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            return false;
        }
        window.PreClose();
        window.windows.Remove(window);
        window.PostClose();
        return true;
    }

    public void Add(WindowUI window)
    {
        window.PreOpen();
        InsertAtCorrectPositionInList(window);
        FocusAfterInsertIfShould(window);
        if(!changeWindowsFocusOrderLater) // neu nhu dell co winUi nao can phai tap trung khi khoi tao thi tap trung vao no
        {
            changeWindowsFocusOrderLater = true;
        }
        window.PostOpen();
    }
    private void InsertAtCorrectPositionInList(WindowUI window)
    {
        int index = 0;
        for (int i = 0; i < this.windows.Count; i++)
        {
            if (window.layer >= this.windows[i].layer)
            {
                index = i + 1;
            }
        }
        this.windows.Insert(index, window);
    }
    private void FocusAfterInsertIfShould(WindowUI window)
    {
        if (!window.focusWhenOpened)
        {
            return;
        }
        if(window != focusedWindow)
        {
            focusedWindow = window;
            changeWindowsFocusOrderLater=true;
        }else
        {
            return;
        }
    }

    
    private static WindowUI focusedWindow;
    private static bool changeWindowsFocusOrderLater;

    protected UIRoot uiRoot;
    public WindowUI parent;
    public List<WindowUI> windows = new List<WindowUI>();


    protected int layer;


    public bool focusWhenOpened = true;
    public bool closeOnAccept = true;
	public bool closeOnCancel = true;
    public bool forceCatchAcceptAndCancelEventEvenIfUnfocused;

    public bool absorbInputAroundWindow;
}
