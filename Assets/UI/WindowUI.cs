using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUI
{
    public WindowUI()
    {
        this.innerWindowOnGUICached = new GUI.WindowFunction(this.InnerWindow);
        this.CommonSearchChangedCached = new Action(this.CommonSearchChanged);
    }

    public void SetParent(WindowUI parent)
    {
        this.parent = parent;
    }
    public void SetRoot(UIRoot _uiRoot)
    {
        uiRoot = _uiRoot;
    }
    

    public virtual Vector2 InitialSize
    {
        get
        {
            return new Vector2(500f, 500f);
        }
    }
    protected virtual float Margin
    {
        get
        {
            return 18f;
        }
    }
    public virtual void InnerWindow(int x)
    {
        getInstance = this;
        if(dontDoAnything) return;

        Rect rect = new(0f,0f,windowRect.width,windowRect.height);
        CurrDrawWindow = this;
        
        {
            GUIStyle backgroundStyle = new();
            Color color = new(0,0,0,0.7f);
            Texture2D background = new(1,1)
            {
                name = "backgroundUI",
            };
            backgroundStyle.normal.background = background;
            background.SetPixel(0, 0, color);
            background.Apply();

            GUI.Label(rect, background, backgroundStyle);
        }

        // if (KeyBindingOf.Cancel.GetKeyDown)
        // {
        //     Current.SceneRoot.uiRoot.windowUI.Notify_PressedCancel();
        // }
        // if (KeyBindingOf.Accept.GetKeyDown)
        // {
        //     Current.SceneRoot.uiRoot.windowUI.Notify_PressedAccept();
        // }
        if (Event.current.type == EventType.MouseDown)
        {
            Current.SceneRoot.uiRoot.windowUI.ClickedInsideWindow(this);
        }
        if (Event.current.type == EventType.KeyDown && !Current.SceneRoot.uiRoot.windowUI.GetsInput(this))
		{
			Event.current.Use();
		}
        if (!string.IsNullOrEmpty(this.optionalTitle))
		{
			GUI.Label(new Rect(this.Margin, this.Margin, this.windowRect.width, 25f), this.optionalTitle);
		}
        Rect rect2 = new Rect(rect.x + this.Margin, rect.y + this.Margin, rect.width - this.Margin * 2f, rect.height - this.Margin * 2f);
        if (!string.IsNullOrEmpty(this.optionalTitle))
		{
			rect2.yMin += this.Margin + 25f;
		}

        GUI.BeginGroup(rect2);
        try
		{
			this.DoWindowContents(new Rect(0f,0f,rect2.width,rect2.height));
		}catch (Exception ex)
        {
            Debug.LogError(string.Concat(new object[]
            {
                "Exception filling window for ",
                base.GetType(),
                ": ",
                ex
            }));
        }
        GUI.EndGroup();


        CurrDrawWindow = null;
    }


    public virtual void DoWindowContents(Rect inRect){}
    public virtual void OnGUI()
    {
        if(dontDoAnything) return;

        if (this.resizeable)
        {
            if (this.resizer == null)
            {
                this.resizer = new WindowResizer();
            }
            if (this.resizeLater)
            {
                this.resizeLater = false;
                this.windowRect = this.afterResizeRect;
            }
        }
        this.windowRect = new Rect((float)((int)windowRect.x), (float)((int)windowRect.y), (float)((int)windowRect.width), (float)((int)windowRect.height));
        this.windowRect = GUI.Window(this.ID, this.windowRect, this.innerWindowOnGUICached, "", new GUIStyle());
    }
    public virtual void PreOpen()
    {
        this.SetInitialSizeAndPosition();
    }
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
    public virtual void OnAcceptKeyPressed()
    {
        if (this.closeOnAccept)
        {
            this.Close(true);
            Event.current.Use();
        }
    }

    protected virtual void SetInitialSizeAndPosition()
    {
        Vector2 initialSize = this.InitialSize;
        this.windowRect = new Rect(((float)UI.screenWidth - initialSize.x) / 2f, ((float)UI.screenHeight - initialSize.y) / 2f, initialSize.x, initialSize.y);
        this.windowRect = new Rect((float)((int)windowRect.x), (float)((int)windowRect.y), (float)((int)windowRect.width), (float)((int)windowRect.height));
    }









    public virtual void CommonSearchChanged()
	{
	}

    public virtual void Close(bool doCloseSound = true)
    {
        
        if(parent != null)
        {
            parent.TryRemove(this,doCloseSound);
        }else
        {
            uiRoot?.windowUI.TryRemove(this, doCloseSound);
        }
    }

    #region Func,Method used on Scene //===========================================================================================\\
    public void WindowUpdate()
    {
        for(int i = 0 ; i<this.windows.Count;i++)
        {
            windows[i].WindowUpdate();
        }
    }
    public void WindowONGUI()
    {
        OnGUI();
        for(int i = 0; i < this.windows.Count;i++)
        {
            windows[i].WindowONGUI();
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
        if(parent != null)
        {
            for (int i = parent.windows.Count - 1; i >= 0; i--)
			{
				if ((parent.windows[i].closeOnCancel || parent.windows[i].forceCatchAcceptAndCancelEventEvenIfUnfocused) && this.GetsInput(parent.windows[i]))
				{
					parent.windows[i].OnCancelKeyPressed();
					return;
				}
			}
        }else
        {
            WindowUI win = Current.SceneRoot.uiRoot.windowUI;
            for (int i = win.windows.Count - 1; i >= 0; i--)
			{
				if ((win.windows[i].closeOnCancel || win.windows[i].forceCatchAcceptAndCancelEventEvenIfUnfocused) && this.GetsInput(win.windows[i]))
				{
					win.windows[i].OnCancelKeyPressed();
					return;
				}
			}
        }
    }
    public void Notify_PressedAccept()
    {
        if(parent != null)
        {
            for (int i = windows.Count - 1; i >= 0; i--)
            {
                if ((parent.windows[i].closeOnAccept || parent.windows[i].forceCatchAcceptAndCancelEventEvenIfUnfocused) && this.GetsInput(parent.windows[i]))
                {
                    parent.windows[i].OnAcceptKeyPressed();
                    return;
                }
            }
        }else
        {
            WindowUI win = Current.SceneRoot.uiRoot.windowUI;
            for (int i = win.windows.Count - 1; i >= 0; i--)
			{
				if ((win.windows[i].closeOnAccept || win.windows[i].forceCatchAcceptAndCancelEventEvenIfUnfocused) && this.GetsInput(win.windows[i]))
				{
					win.windows[i].OnAcceptKeyPressed();
					return;
				}
			}
        }
    }
    
    public bool WindowsForcePause
	{
		get
		{
            uiRoot ??= Current.SceneRoot.uiRoot;
                
			for (int i = 0; i < uiRoot.windowUI.windows.Count; i++)
			{
				if (this.windows[i].PauseWhenOpen)
				{
					return true;
				}
			}
			return false;
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
    //Waring: have a bug here when we used both in 
    public bool TryRemove(WindowUI window, bool doCloseSound = true)
    {
        bool flag = false;
        bool RemoveInParent = false;
        if(parent != null)
        {
            for (int i = 0; i < parent.windows.Count; i++)
            {
                if (parent.windows[i] == window)
                {
                    flag = true;
                    RemoveInParent = true;
                    break;
                }
            }
        }else
        {
            
            WindowUI _ = uiRoot.windowUI;
            for (int i = 0; i < _.windows.Count; i++)
            {
                if (_.windows[i] == window)
                {
                    flag = true;
                    break;
                }
            }
            
        }
         
        if (!flag)
        {
            Debug.Log("Can't Removing UI");
            return false;
        }
        window.PreClose();

        if(RemoveInParent){parent.windows.Remove(window);}
        else {uiRoot?.windowUI.windows.Remove(window);}

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

    public WindowUI GetWindowAt(Vector2 pos,bool tryTofindAll = false)
    {
        {
            for (int i = uiRoot.windowUI.windows.Count - 1; i >= 0; i--)
            {
                if (this.windows[i].windowRect.Contains(pos))
                {
                    return this.windows[i];
                }
                if(tryTofindAll)
                {
                    windows[i].GetWindowAt(pos,tryTofindAll);
                }
            }
        }
        return null;
    }

    public void ClickedInsideWindow(WindowUI window)
    {
        if (this.GetsInput(window))
        {
            this.windows.Remove(window);
            this.InsertAtCorrectPositionInList(window);
            focusedWindow = window;
        }
        else
        {
            Event.current.Use();
        }
    }

     

    public bool HasChildWindow
    {
        get
        {
            return windows.Count > 0;
        }
    }


    public bool MouseObscuredNow
    {
        get
        {
            return this.GetWindowAt(UI.MousePosUIInvertedUseEventIfCan) != CurrDrawWindow;
        }
    }

    public bool CurrentWindowGetsInput
    {
        get
        {
            return this.GetsInput(CurrDrawWindow);
        }
    }




    #endregion




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

    private static UIRoot uiRoot;

    private GUI.WindowFunction innerWindowOnGUICached;
    private Action CommonSearchChangedCached;
    private WindowResizer resizer;

    public int ID; // this is must be fucking unique ID. Idk how to fuck up, just make it random from 0 to 1 bilion.fuck unityGUI

    public WindowUI parent;
    private WindowUI getInstance;
    public List<WindowUI> windows = new();
    public Rect windowRect;

    public static WindowUI CurrDrawWindow;

    protected int layer;


    private bool dontDoAnything 
    {
        get
        {
            return (this==Current.SceneRoot.uiRoot.windowUI && parent == null);
        }
    }  

    public string optionalTitle;

    public bool focusWhenOpened = true;
    public bool closeOnAccept = true;
	public bool closeOnCancel = true;
    public bool forceCatchAcceptAndCancelEventEvenIfUnfocused;

    public bool absorbInputAroundWindow;

    public bool PauseWhenOpen;
    public bool resizeable;
    private bool resizeLater;
    private Rect afterResizeRect;

}
