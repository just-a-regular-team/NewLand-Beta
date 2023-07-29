using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawThingController
{
    public DrawThingController(Map map)
    {
        this.map = map;
    }
    public void RegisterThingToDraw(Thing t)
    {
        this.drawThings.Add(t);
    }
    public void DeregisterThingToDraw(Thing t)
    {
        this.drawThings.Remove(t);
    }


    public void UpdateDrawThing()
    {
        try
        {
            foreach (Thing thing in this.drawThings)
            {
                thing.Draw();
            }
        }
        catch (Exception arg)
        {
            Debug.LogError("Exception drawing things: " + arg);
        }
    }


    public HashSet<Thing> drawThings = new HashSet<Thing>();
    private Map map;
}
