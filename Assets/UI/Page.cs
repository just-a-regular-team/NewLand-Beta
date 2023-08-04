using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : WindowUI
{
    public override Vector2 InitialSize
    {
        get
        {
            return StandardSize;
        }
    }

    public Page()
    {
        this.PauseWhenOpen = true;
        this.absorbInputAroundWindow = true;
        this.closeOnAccept = false;
        this.closeOnCancel = false;
        this.forceCatchAcceptAndCancelEventEvenIfUnfocused = true;
    }
    protected void DrawPageTitle(Rect rect)
    {
        GUI.Label(rect,PageTitle);
    }

    protected void Df_BottomButtons(Rect rect,string nextLabel=null)
    {
        float y = rect.y + rect.height - 38f;
        string label = "Back";
        if ((GUI.Button(new Rect(rect.x, y, Page.BottomButSize.x, Page.BottomButSize.y), label) || KeyBindingOf.Cancel.GetKeyDown))
        {
            this.DoBack();
        }
        if(string.IsNullOrEmpty(nextLabel))
        {
            nextLabel = "Next";
        }
        Debug.Log(this.next == null);
        if (nextLabel == "Next" && (this.next == null))
        {
            nextLabel = "Done";
        }
        Rect rect2 = new Rect(rect.x + rect.width - Page.BottomButSize.x, y, Page.BottomButSize.x, Page.BottomButSize.y);
        if(GUI.Button(rect2, nextLabel))
        {
            this.DoNext();
        }
    }

    protected virtual void DoNext()
    {
        if (this.next != null)
        {
            Current.SceneRoot.uiRoot.windowUI.Add(this.next);
            next.back = this; // remember remove back when it done !!
        }
        if(callActWhenDone)this.actWhenDoneOrQuit?.Invoke();
        this.Close(true);
    }
    protected virtual void DoBack()
    {
        if (this.back != null)
        {
            Current.SceneRoot.uiRoot.windowUI.Add(this.back);
        }
        if(!callActWhenDone)this.actWhenDoneOrQuit?.Invoke();
        this.Close(true);
    }

    public override void OnCancelKeyPressed()
    {
        if (!this.closeOnCancel)
        {
            return;
        }
        if (back != null)
        {
            this.DoBack();
        }
        else
        {
            this.Close(true);
        }
        Event.current.Use();
        base.OnCancelKeyPressed();
    }
    public override void OnAcceptKeyPressed()
    {
        if (!this.closeOnAccept)
        {
            return;
        }
        if(next != null)
        {
            this.DoNext();
        }
        Event.current.Use();
        base.OnAcceptKeyPressed();
    }

    public string PageTitle = string.Empty;

    public static readonly Vector2 StandardSize = new(1020f, 764f);
    protected static readonly Vector2 BottomButSize = new(130f, 40f);

    public Page back;
    public Page next;
    
    public bool callActWhenDone = true;
    public Action actWhenDoneOrQuit;
}
