using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page_NewMap : Page
{
    public Page_NewMap()
    {
        PageTitle = "New Map Page";
    }

    public override void DoWindowContents(Rect inRect)
    {
        base.DrawPageTitle(inRect);
        Df_BottomButtons(inRect);
    }
    protected override void DoNext()
    {
        Current.SetGame = new Game();
        LongEvent.QueueLongEvent(
            delegate(){},"play","Start Playing",true,null
        );
        base.DoNext();
    }
}
