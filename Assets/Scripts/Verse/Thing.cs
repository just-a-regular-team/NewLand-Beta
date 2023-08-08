using System;
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
        obj = new GameObject(name);
        obj.transform.position = this.position;
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
        this.Graphic.DrawMesh(this);
    }

    public Graphic DefaultGraphic
    {
        get
        {
            if (this.graphicInt == null)
            {
                if (this.data.GraphicData == null)
                {
                    Debug.LogError("GraphicData of DataOfThing is null and have no solution now for this - check it out");
                    return null; // try return defaulGraphicData here or find somthing like that in future
                }
                this.graphicInt = this.data.GraphicData.TryFindGraphicFor();
            }
            return this.graphicInt;
        }
    }
    public virtual Graphic Graphic
    {
        get
        {
            return DefaultGraphic;
        }
    }

    public int thingIDNumber = -1;// Id of thing like 000001->999999
    public int globalID;//Id of thing in map like Thing_"ID_On_Map"
    public string name;

    public DataOfThing data;

    public bool Destroyed;

    public Vector3 position;
    public GameObject obj;


    private Graphic graphicInt;
	private Graphic styleGraphicInt;
}
