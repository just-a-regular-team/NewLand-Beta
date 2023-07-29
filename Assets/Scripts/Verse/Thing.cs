using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : Entity
{
    public Map Map
    {
        get{return Map.MapWorking;}
    }
    public override string Name { get => name;set {name = value;} }
    

    public override void SpawnSetup(Map map)
    {
        if(map != Map.MapWorking)
        {
            Debug.Log("map select is not working!");
            return;
        }
        
        Current.GetGamePlaying.GetTimeController.RegisterAllTickabilityFor(this);
        Map.DrawThingController.RegisterThingToDraw(this);
        Destroyed = false;
        
    }

    public override void DeSpawn()
    {
        Current.GetGamePlaying.GetTimeController.DeRegisterAllTickabilityFor(this);
        Map.DrawThingController.DeregisterThingToDraw(this);
        Destroyed = true;
    }

    public virtual void Destroy()
    {
        Current.GetGamePlaying.GetTimeController.DeRegisterAllTickabilityFor(this);
        Map.DrawThingController.DeregisterThingToDraw(this);
        Destroyed = true;
    }

    //Draw thing everyframe by drawThing
    public virtual void Draw()
    {
        DrawThing(position);
    }

    //Maybe update this shit func
    public virtual void DrawThing(Vector3 pos)
    {

    }

    public int thingIDNumber = -1;// Id of thing like 000001->999999
    public int globalID;//Id of thing in map like Thing_"ID_On_Map"
    public string name;

    public DataOfThing data;

    public bool Destroyed;
    public Vector3 position;
}
